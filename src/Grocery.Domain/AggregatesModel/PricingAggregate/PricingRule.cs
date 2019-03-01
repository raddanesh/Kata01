using System;
using Grocery.Domain.SeedWork;

namespace Grocery.Domain.AggregatesModel.PricingAggregate
{
    public class PricingRule : Entity<Guid>
    {
        public string ProductName { get; }
        public Price Price { get; }
        public int Units { get; }

        public PricingRule(Guid id, string productName, int units, Price price) : base(id)
        {
            if (units < 1) { throw new ArgumentOutOfRangeException(nameof(units)); }
            if (string.IsNullOrEmpty(productName)) { throw new ArgumentNullException(nameof(productName)); }
            Price = price ?? throw new ArgumentNullException(nameof(price));

            ProductName = productName;
            Units = units;
        }
    }
}
