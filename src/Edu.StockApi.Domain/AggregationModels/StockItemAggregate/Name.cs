using Edu.StockApi.Domain.Models;

namespace Edu.StockApi.Domain.AggregationModels.StockItemAggregate;

public class Name: ValueObject
{
    public string Value;
    public Name(string name)
    {
        Value = name;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}