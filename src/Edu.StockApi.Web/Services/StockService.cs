namespace Edu.StockApi.Web.Services;

public interface IStockService
{
    public Task<List<StockItem>> GetAll(CancellationToken token);
    public Task<StockItem> GetByID(long id, CancellationToken token);
    public Task<StockItem> Add(StockItemCreationModel stockItem, CancellationToken token);
}

public class StockService : IStockService
{
    private readonly List<StockItem> StockItems = new List<StockItem>
    {
        new StockItem(1, "Футболка", 10),
        new StockItem(2, "Кепка", 20),
        new StockItem(1, "Шарф", 30),
    };

    public Task<List<StockItem>> GetAll(CancellationToken token) => Task.FromResult(StockItems);
    
    public Task<StockItem> GetByID(long id, CancellationToken token){
        var item = StockItems.FirstOrDefault(item => item.ItemId == id);
        return Task.FromResult(item);
    }

    public Task<StockItem> Add(StockItemCreationModel stockItem, CancellationToken token)
    {
        var itemId = StockItems.Max((item => item.ItemId)) + 1;
        var newStockItem = new StockItem(itemId, stockItem.ItemName, stockItem.ItemQuantity);
        StockItems.Add(newStockItem);
        return Task.FromResult(newStockItem);
    }
}


public class StockItemCreationModel
{
    public string ItemName { get; set; }
    public int ItemQuantity { get; set; }
}


public class StockItem
{
    public long ItemId { get; }
    public string ItemName { get; }
    public int ItemQuantity { get; }

    public StockItem(long itemId, string itemName, int itemQuantity)
    {
        ItemId = itemId;
        ItemName = itemName;
        ItemQuantity = itemQuantity;
    }
}


