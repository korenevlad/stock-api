using Edu.StockApi.Domain.AggregationModels.StockItemAggregate;
using MediatR;

namespace Edu.StockApi.Domain.Events;
public class ReachedMinimumStockItemsNumberDomainEvent: INotification
{
    public Sku StockItemSku { get; }

    public ReachedMinimumStockItemsNumberDomainEvent(Sku stockItemSku)
    {
        StockItemSku = stockItemSku;
    }
}