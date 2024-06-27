using AutoMapper;
using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ReserveRoverBLL.DTO.Recommendations;
using ReserveRoverBLL.DTO.Requests;
using ReserveRoverBLL.DTO.Responses;
using ReserveRoverBLL.Enums;
using ReserveRoverBLL.Helpers;
using ReserveRoverBLL.HttpClients;
using ReserveRoverBLL.Services.Abstract;
using ReserveRoverDAL.Entities;
using ReserveRoverDAL.Enums;
using ReserveRoverDAL.UnitOfWork.Abstract;

namespace ReserveRoverBLL.Services.Concrete;

public class PlacesService : IPlacesService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IHostingEnvironment _environment;
    private readonly IIdentityService _identityService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly RecommenderApiClient _recommenderApiClient;

    public PlacesService(IUnitOfWork unitOfWork, IMapper mapper, IHostingEnvironment environment,
        IIdentityService identityService, IHttpContextAccessor httpContextAccessor, RecommenderApiClient recommenderApiClient)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _environment = environment;
        _identityService = identityService;
        _httpContextAccessor = httpContextAccessor;
        _recommenderApiClient = recommenderApiClient;
    }

    public async Task<IEnumerable<PlaceSearchResponse>> Search(PlaceSearchRequest request)
    {
        var result = await _unitOfWork.PlacesRepository.GetAsync(request.cityId, request.titleQuery,
            (short) ModerationStatus.Approved,
            (PlacesSortOrder) request.sortOrder, request.PageNumber, request.PageSize);

        return result.Select(_mapper.Map<Place, PlaceSearchResponse>);
    }
    
    public async Task<IEnumerable<PlaceSearchResponse>> GetRecommendedPlaces(int cityId)
    {
        var userId = UserClaimsHelper.GetUserId(_httpContextAccessor.HttpContext!);
        var userReservationsIds = _unitOfWork.ReservationsRepository.GetAllAsIQueryable()
            .Where(reservation => reservation.UserId == userId)
            .OrderByDescending(reservation => reservation.CreationDateTime)
            .Select(reservation => reservation.PlaceId)
            .Distinct()
            .Take(10)
            .ToHashSet();

        var potentialPlaces = await _unitOfWork.PlacesRepository.GetAllAsIQueryable()
            .Where(place => place.CityId == cityId && place.ModerationStatus == (short) ModerationStatus.Approved &&
                            !userReservationsIds.Contains(place.Id))
            .OrderByDescending(place => place.Popularity)
            .ThenByDescending(place => place.PublicDate)
            .Take(300)
            .ToListAsync();

        var scores = await _recommenderApiClient.RequestRecommendations(new GetRecommendationsDto
        {
            UserId = userId, PotentialPlacesIds = potentialPlaces.Select(place => place.Id)
        });
        
        var orderedPlaces = potentialPlaces.OrderByDescending(place => scores[place.Id]).Take(100);
        return orderedPlaces.Select(_mapper.Map<Place, PlaceSearchResponse>);
    }

    private async Task<PlaceDetailsResponse> PlaceToPlaceDetails(Place place)
    {
        var response = _mapper.Map<Place, PlaceDetailsResponse>(place);

        var images = await _unitOfWork.PlaceImagesRepository.GetAsync(place.Id);
        response.ImageUrls = images.Select(i => i.ImageUrl).ToArray();

        var paymentMethods = await _unitOfWork.PlacesPaymentMethodsRepository.GetByPlaceIdAsync(place.Id);
        response.PaymentMethods = paymentMethods.Select(pm => pm.Method).ToArray();

        return response;
    }

    public async Task<PlaceDetailsResponse> GetPlaceDetails(int placeId)
    {
        var place = await _unitOfWork.PlacesRepository.GetByIdAsync(placeId);
        return await PlaceToPlaceDetails(place);
    }

    public async Task<IEnumerable<ReviewResponse>> GetPlaceReviews(GetPlaceReviewsRequest request)
    {
        var reviews =
            await _unitOfWork.ReviewsRepository.GetByPlaceAsync(request.PlaceId, request.PageNumber, request.PageSize);
        var results = new List<ReviewResponse>();
        var reviewsList = reviews.ToList();

        var userIdentifiers = reviewsList.Select(review => new UidIdentifier(review.AuthorId)).ToList();
        var usersResult = await _identityService.GetUsersById(userIdentifiers);
        var authors = usersResult.Users.ToDictionary(user => user.Uid, user => user);

        foreach (var review in reviewsList)
        {
            var result = _mapper.Map<Review, ReviewResponse>(review);
            result.AuthorPhotoUrl = authors[review.AuthorId].PhotoUrl;
            result.AuthorFullName = authors[review.AuthorId].DisplayName;
            results.Add(result);
        }

        return results;
    }

    public async Task<PlaceDetailsResponse> GetManagersPlace(string managerId)
    {
        var place = await _unitOfWork.PlacesRepository.GetByManagerAsync(managerId);
        return await PlaceToPlaceDetails(place);
    }

    public async Task<string> UploadImage(IFormFile image, HttpContext httpContext)
    {
        var managerId = UserClaimsHelper.GetUserId(httpContext);
        var imagesFolderPath = $"/Images/managers/{managerId}";

        if (!Directory.Exists($"{_environment.WebRootPath}/{imagesFolderPath}/"))
        {
            Directory.CreateDirectory($"{_environment.WebRootPath}/{imagesFolderPath}/");
        }

        var fileExtension = Path.GetExtension(image.FileName);
        var newFileName = $"{DateTime.Now:yyyyMMddHHmmssffff}{fileExtension}";

        await using var fileStream = File.Create($"{_environment.WebRootPath}/{imagesFolderPath}/{newFileName}");
        await image.CopyToAsync(fileStream);
        await fileStream.FlushAsync();

        return $"{imagesFolderPath}/{newFileName}";
    }

    public async Task SetImages(IEnumerable<string> imageUrls, HttpContext httpContext)
    {
        var imagesUrlsList = imageUrls.ToList();
        if (imagesUrlsList.Count < 1)
            throw new ArgumentException("The number of passed images should be greater than zero");

        var mainImageUrl = imagesUrlsList[0];
        imagesUrlsList.RemoveAt(0);
        var managerId = UserClaimsHelper.GetUserId(httpContext);
        var place = await _unitOfWork.PlacesRepository.GetByManagerAsync(managerId);

        await using var transaction = await _unitOfWork.DbContext.Database.BeginTransactionAsync();
        try
        {
            place.MainImageUrl = mainImageUrl;
            await _unitOfWork.SaveChangesAsync();

            await _unitOfWork.PlaceImagesRepository.DeleteByPlaceAsync(place.Id);
            await _unitOfWork.SaveChangesAsync();

            if (imagesUrlsList.Count >= 1)
            {
                var placeImages = imagesUrlsList.Select((imageUrl, index) => new PlaceImage
                    {PlaceId = place.Id, SequenceIndex = (short) index, ImageUrl = imageUrl});
                await _unitOfWork.PlaceImagesRepository.InsertRangeAsync(placeImages);
                await _unitOfWork.SaveChangesAsync();
            }

            await transaction.CommitAsync();
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<int> CreatePlace(AddPlaceRequest placeRequest, HttpContext httpContext)
    {
        var place = _mapper.Map<AddPlaceRequest, Place>(placeRequest);
        place.ManagerId = UserClaimsHelper.GetUserId(httpContext);
        place.ImagesCount = (short) placeRequest.ImageUrls.Length;
        place.SubmissionDateTime = DateTime.Now;
        await using var transaction = await _unitOfWork.DbContext.Database.BeginTransactionAsync();
        try
        {
            await _unitOfWork.PlacesRepository.InsertAsync(place);
            await _unitOfWork.SaveChangesAsync();

            await _unitOfWork.PlacesRepository.AddDescription(new PlaceDescription
                {PlaceId = place.Id, Description = placeRequest.Description});

            await _unitOfWork.PlacesPaymentMethodsRepository.InsertRangeAsync(
                placeRequest.PaymentMethods.Select(method =>
                    new PlacePaymentMethod {PlaceId = place.Id, Method = method}));

            await _unitOfWork.PlaceImagesRepository.InsertRangeAsync(
                placeRequest.ImageUrls.Select((url, index) =>
                    new PlaceImage {PlaceId = place.Id, ImageUrl = url, SequenceIndex = (short) index}));

            if (placeRequest.Location != null)
            {
                var location = _mapper.Map<AddPlaceLocationRequest, Location>(placeRequest.Location);
                location.PlaceId = place.Id;
                await _unitOfWork.LocationsRepository.InsertAsync(location);
            }

            await _unitOfWork.SaveChangesAsync();
            await transaction.CommitAsync();
            return place.Id;
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<IEnumerable<TableSetResponse>> GetPlaceTableSets(int placeId)
    {
        var entities = await _unitOfWork.TableSetsRepository.GetByPlaceAsync(placeId);
        return entities.Select(_mapper.Map<TableSet, TableSetResponse>);
    }

    public async Task<bool> SetPlaceTableSets(SetPlaceTableSetsRequest request)
    {
        var tableSetsToInsert = new List<TableSet>();
        var tableSetsToUpdate = new List<TableSet>();
        foreach (var tableSetRequest in request.TableSets)
        {
            var tableSet = _mapper.Map<SetPlaceTableSetsRequest.TableSetRequest, TableSet>(tableSetRequest);
            if (tableSetRequest.Id == null)
                tableSetsToInsert.Add(tableSet);
            else
                tableSetsToUpdate.Add(tableSet);
        }

        await using var transaction = await _unitOfWork.DbContext.Database.BeginTransactionAsync();
        try
        {
            await _unitOfWork.TableSetsRepository.UpdateRangeAsync(tableSetsToUpdate);
            await _unitOfWork.TableSetsRepository.InsertRangeAsync(tableSetsToInsert);
            await _unitOfWork.TableSetsRepository.DeleteByIdRangeAsync(request.IdsToDelete);
            await _unitOfWork.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }

        return true;
    }

    public async Task<ReviewResponse> CreateReview(CreatePlaceReviewRequest request, HttpContext httpContext)
    {
        var userId = UserClaimsHelper.GetUserId(httpContext);
        // const string userId = "M34";
        var review = _mapper.Map<CreatePlaceReviewRequest, Review>(request);
        review.AuthorId = userId;
        review.CreationDate = DateOnly.FromDateTime(DateTime.Now);
        await _unitOfWork.ReviewsRepository.InsertAsync(review);
        await _unitOfWork.SaveChangesAsync();

        var result = _mapper.Map<Review, ReviewResponse>(review);
        var author = await _identityService.GetUserById(review.AuthorId);
        result.AuthorFullName = author.DisplayName;
        result.AuthorPhotoUrl = author.PhotoUrl;
        return result;
    }
}