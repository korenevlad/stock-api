using Edu.StockApi.Domain.Models;

namespace Edu.StockApi.Domain.AggregationModels.StockItemAggregate;

public class Item : Entity
{
    public ItemType Type { get; }
    public Item(ItemType type)
    {
        Type = type;
    }
}