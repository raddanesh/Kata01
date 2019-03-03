using System;
using Grocery.Domain.SeedWork;

namespace Grocery.Domain.AggregatesModel.PricingAggregate
{
    public class VolumePricingRule : Entity<Guid>, IAggregateRoot
    {
        public VolumePricingRule(Guid id, Guid productId, int units, Price price) : base(id)
        {
            if (units < 1) { throw new ArgumentOutOfRangeException(nameof(units)); }
            Price = price ?? throw new ArgumentNullException(nameof(price));

            ProductId = productId;
            Units = units;
        }

        public Guid ProductId { get; }
        public Price Price { get; }
        public int Units { get; }
    }
}