using Edu.StockApi.Domain.Events;
using Edu.StockApi.Domain.Models;

namespace Edu.StockApi.Domain.AggregationModels.StockItemAggregate;

public class StockItem : Entity
{
    public Sku Sku { get; }
    public Name Name { get; }
    public Item ItemType { get; }
    public ClothingSize ClothingSize { get; private set; }
    public Quantity Quantity { get; private set; }
    public Quantity MinimalQuantity { get; private set; }
    public Tag Tag { get; }
    
    public StockItem(Sku sku, Name name, Item itemType, 
        ClothingSize size, Quantity quantity, Quantity minimalQuantity, Tag tag)
    {
        Sku = sku;
        Name = name;
        ItemType = itemType;
        SetClothingSize(size);
        Quantity = quantity;
        MinimalQuantity = minimalQuantity;
        Tag = tag;
    }

    public void IncreaseQuantity(int valueToIncrease)
    {
        if (valueToIncrease < 0)
        {
            throw new Exception($"{nameof(valueToIncrease)} value is negative");
        }
        Quantity = new Quantity(this.Quantity.Value + valueToIncrease);
    }
    
    public void GiveOutItems(int valueToGiveOut)
    {
        if (valueToGiveOut < 0)
        {
            throw new Exception($"{nameof(valueToGiveOut)} value is negative");
        }
        if (Quantity.Value < valueToGiveOut)
        {
            throw new Exception("Not enouth items");
        }
        Quantity = new Quantity(this.Quantity.Value - valueToGiveOut);
        if (Quantity.Value <= MinimalQuantity.Value)
        {
            AddReachedMinimumDomainEvent(Sku);
        }
    }
    private void SetClothingSize(ClothingSize clothingSize)
    {
        if (clothingSize is not null && (
                ItemType.Type.Equals(StockItemAggregate.ItemType.TShirt) ||
                                     ItemType.Type.Equals(StockItemAggregate.ItemType.Sweatshirt)))
            ClothingSize = clothingSize;
        else if (clothingSize is null)
        {
            ClothingSize = null;
        }
        else
        {
            throw new Exception($"Item with type {ItemType.Type.Name} cannot contain size");
        }
    }
    private void AddReachedMinimumDomainEvent(Sku sku)
    {
        var orderStartDomainEvent = new ReachedMinimumStockItemsNumberDomainEvent(sku);
        this.AddDomainEvent(orderStartDomainEvent);
    }
}