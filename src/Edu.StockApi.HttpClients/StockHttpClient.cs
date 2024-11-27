using System.Text.Json;
using Edu.StockApi.HttpModels;

namespace Edu.StockApi.HttpClients;
public interface IStockHttpClient { }

public class StockHttpClient: IStockHttpClient
{
    private readonly HttpClient _httpClient;
    public StockHttpClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<StockItemResponse>> GetAll(CancellationToken token)
    {
        using var response = await _httpClient.GetAsync("v1/api/stocks", token);
        var body = await response.Content.ReadAsStringAsync(token);
        return JsonSerializer.Deserialize<List<StockItemResponse>>(body);
    }
}