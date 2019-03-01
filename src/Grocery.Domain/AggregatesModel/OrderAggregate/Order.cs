using System;
using System.Collections.Generic;
using System.Linq;
using Grocery.Domain.AggregatesModel.PricingAggregate;
using Grocery.Domain.AggregatesModel.ProductAggregate;
using Grocery.Domain.SeedWork;

namespace Grocery.Domain.AggregatesModel.OrderAggregate
{
    public class Order : Entity<Guid>, IAggregateRoot
    {
        private readonly List<OrderItem> _orderItems;
        private readonly IPricingStrategyFactory _pricingStrategyFactory;

        /// <summary>
        /// Gets a readonly collection of <see cref="OrderItem"/> of the current order.
        /// </summary>
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;


        /// <summary>
        /// Creates a new <see cref="Order"/> instance.
        /// </summary>
        public Order(Guid id, IPricingStrategyFactory pricingStrategyFactory) : base(id)
        {
            _pricingStrategyFactory = pricingStrategyFactory;
            _orderItems = new List<OrderItem>();
        }


        /// <summary>
        /// Adds a <see cref="Product"/> into the OrderItem with one or several <paramref name="units"/>.
        /// </summary>
        /// <param name="product"></param>
        /// <param name="units"></param>
        public void AddOrderItem(Product product, int units = 1)
        {
            var existingOrderForProduct = _orderItems.SingleOrDefault(o => o.ProductId == product.Id);

            if (existingOrderForProduct != null)
            {
                existingOrderForProduct.AddUnits(units);
            }
            else
            {
                var pricingStrategy = _pricingStrategyFactory.Create(product.Name);
                var orderItem = new OrderItem(Guid.NewGuid(), product.Id, product.Name, product.UnitPrice, pricingStrategy, units);

                _orderItems.Add(orderItem);
            }
        }


        /// <summary>
        /// Gets the total price of the current order.
        /// </summary>
        /// <returns>A <see cref="Price"/> object</returns>
        public Price GetTotalPrice()
        {
            return _orderItems.Sum(o => o.GetTotalPrice().Value);
        }
    }
}
