using Xunit;
using Edu.StockApi.Domain.AggregationModels.StockItemAggregate;

namespace Edu.StockApi.Domain.Tests;
public class QuantityValueObjectTests
{
    [Fact]
    public void CreateQuantityInstanceSuccess()
    {
        //Arrage
        var quantity = 10;

        //Act
        var result = new Quantity(10);

        //Assert
        Assert.Equal(quantity, result.Value);
    }
}