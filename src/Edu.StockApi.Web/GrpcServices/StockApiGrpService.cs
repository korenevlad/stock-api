using System.Reflection;
using Edu.StockApi.Grpc;
using Edu.StockApi.Web.Services;
using Google.Protobuf.WellKnownTypes;
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

    public override async Task<GetAllStockItemsWithNullsResponse> GetAllStockItemsWithNulls(Empty request, ServerCallContext context)
    {
        var stockItems = await _stockService.GetAll(context.CancellationToken);
        return new GetAllStockItemsWithNullsResponse
        {
            Stocks = { stockItems.Select(x => new GetAllStockItemsWithNullsResponseUnit 
            {
                ItemId = x.ItemId,
                Quantity = x.ItemQuantity,
                ItemName = x.ItemName
            })}
        };  
    }

    public override async Task<GetAllStockItemsMapResponse> GetAllStockItemsMap(Empty request, ServerCallContext context)
    {
        var stockItems = await _stockService.GetAll(context.CancellationToken);
        return new GetAllStockItemsMapResponse
        {
            Stocks = { stockItems.ToDictionary(x => x.ItemId, x => new GetAllStockItemsResponseUnit 
            {
                ItemId = x.ItemId,
                ItemQuantity = x.ItemQuantity,
                ItemName = x.ItemName
            })}
        };  
    }

    public override Task<Empty> AddStockItem(AddStockItemRequest request, ServerCallContext context)
    {
        throw new RpcException(
            new Status(StatusCode.InvalidArgument, "validation failed"),
            new Metadata { new Metadata.Entry("key", "our value")});
    }
}