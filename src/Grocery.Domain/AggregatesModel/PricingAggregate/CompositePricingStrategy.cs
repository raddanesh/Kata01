using System.Collections.Generic;
using Grocery.Domain.AggregatesModel.OrderAggregate;

namespace Grocery.Domain.AggregatesModel.PricingAggregate
{
    public abstract class CompositePricingStrategy : IPricingStrategy
    {
        protected List<IPricingStrategy> PricingStrategies;

        protected CompositePricingStrategy()
        {
            PricingStrategies = new List<IPricingStrategy>();
        }

        public abstract Price GetTotal(OrderItem item);

        public void AddPricingStrategy(IPricingStrategy pricingStrategy)
        {
            PricingStrategies.Add(pricingStrategy);
        }
    }
}