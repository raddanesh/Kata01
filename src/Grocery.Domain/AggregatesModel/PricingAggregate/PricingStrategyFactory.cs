namespace Grocery.Domain.AggregatesModel.PricingAggregate
{
    public class PricingStrategyFactory : IPricingStrategyFactory
    {
        private readonly IPricingRulesRepository _pricingRulesRepository;

        public PricingStrategyFactory(IPricingRulesRepository pricingRulesRepository)
        {
            _pricingRulesRepository = pricingRulesRepository;
        }

        public IPricingStrategy Create(string productName)
        {
            var compositePricingStrategy = new CompositeLowestPricingStrategy();

            compositePricingStrategy.AddPricingStrategy(new RegularPricingStrategy());

            var volumeRule = _pricingRulesRepository.GetByProductName(productName);

            if (volumeRule != null)
            {
                compositePricingStrategy.AddPricingStrategy(new VolumePricingStrategy(volumeRule.Units, volumeRule.Price));
            }

            return compositePricingStrategy;
        }
    }
}
