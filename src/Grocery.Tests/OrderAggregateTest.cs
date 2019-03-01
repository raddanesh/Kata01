using System;
using System.Linq;
using Grocery.Domain.AggregatesModel;
using Grocery.Domain.AggregatesModel.OrderAggregate;
using Grocery.Domain.AggregatesModel.PricingAggregate;
using Grocery.Domain.AggregatesModel.ProductAggregate;

using NSubstitute;

using Xunit;

// ReSharper disable InconsistentNaming

namespace Grocery.Tests
{
    public class OrderAggregateTest
    {
        private readonly IPricingRulesRepository _pricingRulesRepository;
        private readonly IPricingStrategyFactory _pricingStrategyFactory;
        private readonly IPricingStrategy _pricingStrategy;

        public OrderAggregateTest()
        {
            _pricingRulesRepository = Substitute.For<IPricingRulesRepository>();
            _pricingStrategy = Substitute.For<IPricingStrategy>();
            _pricingStrategyFactory = new PricingStrategyFactory(_pricingRulesRepository);
        }


        [Fact]
        public void AddOrderItem_SingleProduct_NoDiscount()
        {
            //Arrange
            var C = new Product(Guid.NewGuid(), "C", 1.00m);
            var pricingRuleForC = new PricingRule(Guid.NewGuid(), C.Name, 6, 5.00m);

            _pricingRulesRepository.GetByProductName(C.Name).Returns(pricingRuleForC);

            var order = new Order(Guid.NewGuid(), _pricingStrategyFactory);


            //Act
            order.AddOrderItem(C, 5);

            Price expectedTotalPrice = 5.00m;


            //Assert
            Assert.Equal(expectedTotalPrice, order.GetTotalPrice());
        }


        [Fact]
        public void AddOrderItem_MultipleProduct_NoDiscount()
        {
            //Arrange
            var A = new Product(Guid.NewGuid(), "A", 1.25m);
            var pricingRuleForA = new PricingRule(Guid.NewGuid(), A.Name, 3, 3.00m);
            _pricingRulesRepository.GetByProductName(A.Name).Returns(pricingRuleForA);

            var C = new Product(Guid.NewGuid(), "C", 1.00m);
            var pricingRuleForC = new PricingRule(Guid.NewGuid(), C.Name, 6, 5.00m);
            _pricingRulesRepository.GetByProductName(C.Name).Returns(pricingRuleForC);

            var order = new Order(Guid.NewGuid(), _pricingStrategyFactory);


            //Act
            order.AddOrderItem(A, 2);
            order.AddOrderItem(C, 5);

            Price expectedTotalPrice = 7.50m;


            //Assert
            Assert.Equal(expectedTotalPrice, order.GetTotalPrice());
        }


        [Fact]
        public void AddOrderItem_SingleProduct_ShouldApplyDiscount()
        {
            //Arrange
            var C = new Product(Guid.NewGuid(), "C", 1.00m);
            var pricingRuleForC = new PricingRule(Guid.NewGuid(), C.Name, 6, 5.00m);
            _pricingRulesRepository.GetByProductName(C.Name).Returns(pricingRuleForC);

            var order = new Order(Guid.NewGuid(), _pricingStrategyFactory);


            //Act
            order.AddOrderItem(C, 7);

            Price expectedTotalPrice = 6.00m;


            //Assert
            Assert.Equal(expectedTotalPrice, order.GetTotalPrice());
        }


        [Fact]
        public void AddOrderItem_MultipleProduct_MultipleUnits_ShouldApplyDiscount()
        {
            //Arrange
            var A = new Product(Guid.NewGuid(), "A", 1.25m);
            var pricingRuleForA = new PricingRule(Guid.NewGuid(), A.Name, 3, 3.00m);
            _pricingRulesRepository.GetByProductName(A.Name).Returns(pricingRuleForA);

            var B = new Product(Guid.NewGuid(), "B", 4.25m);
            _pricingRulesRepository.GetByProductName(B.Name).Returns(r => null);

            var C = new Product(Guid.NewGuid(), "C", 1.00m);
            var pricingRuleForC = new PricingRule(Guid.NewGuid(), C.Name, 6, 5.00m);
            _pricingRulesRepository.GetByProductName(C.Name).Returns(pricingRuleForC);

            var D = new Product(Guid.NewGuid(), "D", 0.75m);
            _pricingRulesRepository.GetByProductName(D.Name).Returns(r => null);

            var order = new Order(Guid.NewGuid(), _pricingStrategyFactory);
            


            //Act
            order.AddOrderItem(A);
            order.AddOrderItem(B);
            order.AddOrderItem(C);
            order.AddOrderItem(D);
            order.AddOrderItem(A);
            order.AddOrderItem(B);
            order.AddOrderItem(A);

            Price expectedTotalPrice = 13.25m;


            //Assert
            Assert.Equal(expectedTotalPrice, order.GetTotalPrice());
        }


        [Fact]
        public void AddOrderItem_MultipleProduct_SingleUnit_ShouldApplyDiscount()
        {
            //Arrange
            var A = new Product(Guid.NewGuid(), "A", 1.25m);
            var pricingRuleForA = new PricingRule(Guid.NewGuid(), A.Name, 3, 3.00m);
            _pricingRulesRepository.GetByProductName(A.Name).Returns(pricingRuleForA);

            var B = new Product(Guid.NewGuid(), "B", 4.25m);
            _pricingRulesRepository.GetByProductName(B.Name).Returns(r => null);

            var C = new Product(Guid.NewGuid(), "C", 1.00m);
            var pricingRuleForC = new PricingRule(Guid.NewGuid(), C.Name, 6, 5.00m);
            _pricingRulesRepository.GetByProductName(C.Name).Returns(pricingRuleForC);

            var D = new Product(Guid.NewGuid(), "D", 0.75m);
            _pricingRulesRepository.GetByProductName(D.Name).Returns(r => null);

            var order = new Order(Guid.NewGuid(), _pricingStrategyFactory);


            //Act
            order.AddOrderItem(A);
            order.AddOrderItem(B);
            order.AddOrderItem(C);
            order.AddOrderItem(D);

            Price expectedTotalPrice = 7.25m;


            //Assert
            Assert.Equal(expectedTotalPrice, order.GetTotalPrice());
        }


        [Fact]
        public void AddOrderItem_ShouldApplyDiscount_OnlyOnce()
        {
            //Arrange
            var C = new Product(Guid.NewGuid(), "C", 1.00m);
            var pricingRuleForC = new PricingRule(Guid.NewGuid(), C.Name, 6, 5.00m);
            _pricingRulesRepository.GetByProductName(C.Name).Returns(pricingRuleForC);

            var order = new Order(Guid.NewGuid(), _pricingStrategyFactory);


            //Act
            order.AddOrderItem(C, 12);

            Price expectedTotalPrice = 11.00m;


            //Assert
            Assert.Equal(expectedTotalPrice, order.GetTotalPrice());
        }


        [Fact]
        public void OrderItem_Invalid_Units()
        {
            //Arrange
            var productId = Guid.NewGuid();
            var productName = "A";
            const int unitPrice = 12;
            const int units = -1;


            //Act - Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => new OrderItem(Guid.NewGuid(), productId, productName, unitPrice, _pricingStrategy, units));
        }


        [Fact]
        public void OrderItem_Invalid_Units_Setting()
        {
            //Arrange
            var productId = Guid.NewGuid();
            var productName = "A";
            const int unitPrice = 12;
            var fakeOrderItem = new OrderItem(Guid.NewGuid(), productId, productName, unitPrice, _pricingStrategy);


            //Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => fakeOrderItem.AddUnits(-1));
        }


        [Fact]
        public void OrderItem_AddUnits_UpdatesUnits()
        {
            //Arrange
            var productId = Guid.NewGuid();
            var productName = "A";
            const int unitPrice = 12;
            var fakeOrderItem = new OrderItem(Guid.NewGuid(), productId, productName, unitPrice, _pricingStrategy);


            //Act
            fakeOrderItem.AddUnits(1);

            const int expectedUnits = 2;


            //Assert
            Assert.Equal(expectedUnits, fakeOrderItem.GetUnits());
        }


        [Fact]
        public void Order_AddOrderItem_ExistingOrderItem_UpdatesUnits()
        {
            //Arrange
            const int unitPrice = 12;
            var fakeProduct = new Product(Guid.NewGuid(), "fakeProduct", unitPrice);
            var fakeOrder = new Order(Guid.NewGuid(), _pricingStrategyFactory);

            //Act
            fakeOrder.AddOrderItem(fakeProduct);
            fakeOrder.AddOrderItem(fakeProduct);

            const int orderItems = 1;
            const int expectedUnits = 2;

            //Assert
            Assert.Equal(orderItems, fakeOrder.OrderItems.Count);
            Assert.Equal(expectedUnits, fakeOrder.OrderItems.First().GetUnits());
        }
    }
}
