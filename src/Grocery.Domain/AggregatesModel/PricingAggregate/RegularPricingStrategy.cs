using Grocery.Domain.AggregatesModel.OrderAggregate;

namespace Grocery.Domain.AggregatesModel.PricingAggregate
{
    public class RegularPricingStrategy : IPricingStrategy
    {
        public Price GetTotal(OrderItem item)
        {
            return item.GetUnits() * item.GetUnitPrice();
        }
    }
}
