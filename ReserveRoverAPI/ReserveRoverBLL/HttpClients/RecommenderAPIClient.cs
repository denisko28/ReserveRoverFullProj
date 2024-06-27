using System.Text;
using Newtonsoft.Json;
using ReserveRoverBLL.DTO.Recommendations;

namespace ReserveRoverBLL.HttpClients;

public class RecommenderApiClient
{
    private readonly HttpClient _httpClient;

    public RecommenderApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<IDictionary<int, float>> RequestRecommendations(GetRecommendationsDto model)
    {
        var json = JsonConvert.SerializeObject(model);
        var data = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("/predict", data);
        var result = await response.Content.ReadAsStringAsync();
        var predictions = JsonConvert.DeserializeObject<IDictionary<int, float>>(result);
        return predictions!;
    }
}