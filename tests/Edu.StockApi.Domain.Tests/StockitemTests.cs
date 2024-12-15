using Edu.StockApi.Domain.AggregationModels.StockItemAggregate;

namespace Edu.StockApi.Domain.Tests;

public class StockitemTests
{
    [Fact]
    public void IncreaseStockItemQuantity()
    {
        //Arrange
        var stockItem = new StockItem(
            new Sku(129342),
            new Name("Some Name"),
            new Item(ItemType.TShirt),
            ClothingSize.S,
            new Quantity(10),
            new Quantity(5),
            new Tag("Some tag"));

        var valueToIncrease = 10;

        //Act
        stockItem.IncreaseQuantity(valueToIncrease);

        //Assert
        Assert.Equal(20, stockItem.Quantity.Value);
    }
    
    [Fact]
    public void IncreaseQuantityNegativeValueSuccess()
    {
        //Arrange
        var stockItem = new StockItem(
            new Sku(129342),
            new Name("Some Name"),
            new Item(ItemType.TShirt),
            ClothingSize.S,
            new Quantity(10),
            new Quantity(5),
            new Tag("Some tag"));

        var valueToIncrease = -10;

        //Act

        //Assert
        Assert.Throws<Exception>(() => stockItem.IncreaseQuantity(valueToIncrease));
    }
}