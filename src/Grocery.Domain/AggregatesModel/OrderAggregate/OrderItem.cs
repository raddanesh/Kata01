using System;
using Grocery.Domain.AggregatesModel.PricingAggregate;
using Grocery.Domain.SeedWork;

namespace Grocery.Domain.AggregatesModel.OrderAggregate
{
    public class OrderItem : Entity<Guid>
    {
        public Guid ProductId { get; }

        private readonly IPricingStrategy _pricingStrategy;

        private readonly string _productName;
        private readonly Price _unitPrice;
        private int _units;

        public OrderItem(Guid id, Guid productId, string productName, Price unitPrice, IPricingStrategy pricingStrategy, int units = 1) : base(id)
        {
            if (units < 1) { throw new ArgumentOutOfRangeException(nameof(units)); }
            if (unitPrice == null) { throw new ArgumentNullException(nameof(unitPrice)); }
            if (unitPrice.Value < 0) { throw new ArgumentOutOfRangeException(nameof(unitPrice)); }

            _pricingStrategy = pricingStrategy ?? throw new ArgumentNullException(nameof(pricingStrategy));

            ProductId = productId;

            _productName = productName;
            _unitPrice = unitPrice;
            _units = units;
        }

        /// <summary>
        /// Gets the unit number of the OrderItem's product.
        /// </summary>
        /// <returns>A <see cref="int"/>.</returns>
        public int GetUnits()
        {
            return _units;
        }

        /// <summary>
        /// Gets the unit price of the OrderItem's product.
        /// </summary>
        /// <returns>A <see cref="Price"/> object</returns>
        public Price GetUnitPrice()
        {
            return _unitPrice;
        }

        /// <summary>
        /// Gets the unit price of the OrderItem's product.
        /// </summary>
        /// <returns>A <see cref="Price"/> object</returns>
        public Price GetTotalPrice()
        {
            return _pricingStrategy.GetTotal(this);
        }

        /// <summary>
        /// Adds <paramref name="units" /> of the existing product in the OrderItem.
        /// </summary>
        /// <param name="units"></param>
        public void AddUnits(int units)
        {
            if (units < 1) { throw new ArgumentOutOfRangeException(nameof(units)); }

            _units += units;
        }
    }
}
