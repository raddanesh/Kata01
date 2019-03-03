using System;

namespace Grocery.Domain.AggregatesModel.PricingAggregate
{
    public interface IPricingStrategyFactory
    {
        IPricingStrategy Create(Guid productId);
    }
}