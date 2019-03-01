namespace Grocery.Domain.AggregatesModel.PricingAggregate
{
    public interface IPricingRulesRepository
    {
        PricingRule GetByProductName(string productName);
    }
}
