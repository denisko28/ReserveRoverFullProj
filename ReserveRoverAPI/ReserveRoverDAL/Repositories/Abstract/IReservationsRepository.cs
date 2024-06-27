using ReserveRoverDAL.Entities;

namespace ReserveRoverDAL.Repositories.Abstract;

public interface IReservationsRepository
{
    Task<IEnumerable<Reservation>> GetByPlaceAsync(int placeId, DateTime? fromTime, DateTime? tillTime, int pageNumber, int pageSize);

    Task<IEnumerable<Reservation>> GetByUserAsync(string userId, DateTime? fromTime, DateTime? tillTime, int pageNumber, int pageSize);

    Task<IEnumerable<Reservation>> GetByTableSetIdAndReservDateAsync(int tableSetId, DateOnly reservDate);
    
    IQueryable<Reservation> GetAllAsIQueryable();
    
    Task<int> CountTillDateByUserAsync(string userId, DateTime dateTime);

    Task<int> CountFromDateByUserAsync(string userId, DateTime dateTime);
    
    Task InsertAsync(Reservation reservation);

    Task UpdateStatusAsync(string id, short status);
}