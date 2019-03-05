using Grocery.Domain.AggregatesModel.OrderAggregate;

namespace Grocery.Domain.AggregatesModel.PricingAggregate
{
    public interface IPricingStrategy
    {
        Price GetTotal(IOrderItemContext item);
    }
}