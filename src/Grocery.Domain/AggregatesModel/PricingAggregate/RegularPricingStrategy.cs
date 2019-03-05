using Grocery.Domain.AggregatesModel.OrderAggregate;

namespace Grocery.Domain.AggregatesModel.PricingAggregate
{
    public class RegularPricingStrategy : IPricingStrategy
    {
        public virtual Price GetTotal(IOrderItemContext item)
        {
            return item.GetUnits() * item.GetUnitPrice();
        }
    }
}