using Edu.StockApi.Grpc;
using Grpc.Core;
using Grpc.Net.Client;

using var chanel = GrpcChannel.ForAddress("https://localhost:7266");
var client = new StockApiGrpc.StockApiGrpcClient(chanel);

// var response = await client.GetAllStockItemsAsync(new GetAllStockItemsRequest(), cancellationToken: CancellationToken.None);
// foreach (var item in response.Stocks)
// {
//     Console.WriteLine($"Item id {item.ItemId} - Quantity {item.ItemQuantity}");
// }

try
{
    await client.AddStockItemAsync(new AddStockItemRequest { ItemQuantity = 1, ItemName = "Item to add" });
}
catch (RpcException ex)
{
    var metadata = ex.Trailers;
    metadata.FirstOrDefault(x => x.Key == "key");
    Console.WriteLine(ex);
}