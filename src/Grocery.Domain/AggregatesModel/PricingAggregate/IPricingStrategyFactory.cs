namespace Grocery.Domain.AggregatesModel.PricingAggregate
{
    public interface IPricingStrategyFactory
    {
        IPricingStrategy Create(string productName);
    }
}
