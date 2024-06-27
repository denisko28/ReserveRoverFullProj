namespace ReserveRoverBLL.DTO.Recommendations;

public class GetRecommendationsDto
{
    public string UserId { get; set; }
    
    public IEnumerable<int> PotentialPlacesIds { get; set; }
}