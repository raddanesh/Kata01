using Grocery.Domain.AggregatesModel.OrderAggregate;

namespace Grocery.Domain.AggregatesModel.PricingAggregate
{
    public class CompositeLowestPricingStrategy : CompositePricingStrategy
    {
        public override Price GetTotal(IOrderItemContext item)
        {
            Price lowestPrice = decimal.MaxValue;

            foreach (var pricingStrategy in PricingStrategies)
            {
                var total = pricingStrategy.GetTotal(item);

                if (total < lowestPrice)
                {
                    lowestPrice = total;
                }
            }

            return lowestPrice;
        }
    }
}