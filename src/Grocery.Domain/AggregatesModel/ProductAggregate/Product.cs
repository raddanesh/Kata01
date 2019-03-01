using System;
using Grocery.Domain.SeedWork;

namespace Grocery.Domain.AggregatesModel.ProductAggregate
{
    public class Product : Entity<Guid>, IAggregateRoot
    {
        public string Name { get; private set; }
        public Price UnitPrice { get; private set; }

        /// <summary>
        /// Creates a new <see cref="Product"/> instance.
        /// </summary>
        public Product(Guid id, string name, Price unitPrice) : base(id)
        {
            if (string.IsNullOrEmpty(name)) { throw new ArgumentNullException(nameof(name)); }
            if (unitPrice == null) { throw new ArgumentNullException(nameof(unitPrice)); }
            if (unitPrice.Value < 0) { throw new ArgumentOutOfRangeException(nameof(unitPrice)); }

            Name = name;
            UnitPrice = unitPrice;
        }

        /// <summary>
        /// Sets the unit price of the product
        /// </summary>
        /// <param name="unitPrice"></param>
        public void SetUnitPrice(Price unitPrice)
        {
            UnitPrice = unitPrice;
        }
    }
}
