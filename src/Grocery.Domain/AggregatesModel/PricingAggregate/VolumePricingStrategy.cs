using System;
using Grocery.Domain.AggregatesModel.OrderAggregate;

namespace Grocery.Domain.AggregatesModel.PricingAggregate
{
    public class VolumePricingStrategy : IPricingStrategy
    {
        private readonly Price _volumePrice;
        private readonly int _volumeThreshold;

        public VolumePricingStrategy(int volumeThreshold, Price volumePrice)
        {
            if (volumeThreshold < 1) { throw new ArgumentOutOfRangeException(nameof(volumeThreshold)); }
            _volumePrice = volumePrice ?? throw new ArgumentNullException(nameof(volumePrice));

            _volumeThreshold = volumeThreshold;
        }

        public Price GetTotal(IOrderItemContext item)
        {
            var regularPrice = item.GetUnits() * item.GetUnitPrice();
            Price volumeDiscount = 0;

            if (item.GetUnits() >= _volumeThreshold)
            {
                volumeDiscount = _volumeThreshold * item.GetUnitPrice() - _volumePrice;
            }

            return regularPrice - volumeDiscount;
        }
    }
}