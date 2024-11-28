using Edu.StockApi.Grpc;
using Edu.StockApi.Web.Services;
using Grpc.Core;

namespace Edu.StockApi.Web.GrpcServices;
public class StockApiGrpService : StockApiGrpc.StockApiGrpcBase
{
    private readonly IStockService _stockService;

    public StockApiGrpService(IStockService stockService)
    {
        _stockService = stockService;
    }

    public override async Task<GetAllStockItemsResponse> GetAllStockItems (
        GetAllStockItemsRequest request,
        ServerCallContext context)
    {
        var stockItems = await _stockService.GetAll(context.CancellationToken);
        return new GetAllStockItemsResponse
        {
            Stocks = { stockItems.Select(x => new GetAllStockItemsResponseUnit 
            {
                ItemId = x.ItemId,
                ItemQuantity = x.ItemQuantity,
                ItemName = x.ItemName
            })}
        };  
    }
}