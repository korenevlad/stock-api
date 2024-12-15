using Edu.StockApi.Domain.Models;

namespace Edu.StockApi.Domain.AggregationModels.StockItemAggregate;

public class Tag : ValueObject
{
    public string Value { get; }
    public Tag(string value)
    {
        Value = value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}