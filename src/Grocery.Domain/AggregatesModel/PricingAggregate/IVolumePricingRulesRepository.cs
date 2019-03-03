using System;

namespace Grocery.Domain.AggregatesModel.PricingAggregate
{
    public interface IVolumePricingRulesRepository
    {
        VolumePricingRule GetByProductId(Guid productId);
    }
}