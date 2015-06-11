using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using System.Collections.Generic;
using System.Linq;
using E_ShopDomainModel.Interfaces;

namespace E_ShopDomainModel.Tests
{
    [TestClass]
    public class DiscountPolicyTests
    {
        [TestMethod]
        public void When_lower_or_equal_than_minSumForDiscount_and_minForDiscountCoefficient()
        {
            //arrange
            List<ItemEntity> items = new List<ItemEntity>();
            items.Add(
                      new ItemEntity
                      {
                          ItemId = Guid.NewGuid(),
                          Name = "carrot",
                          Description = "delicious",
                          Price = (decimal)2.5,
                          Count = 5
                      }
                     );

            DiscountPolicy discount = new DiscountPolicy();
            var sum = items.Sum<ItemEntity>(x => x.Price * x.Count);

            //act
            var result = discount.PriceCalculation(items);

            //assert
            Assert.AreEqual(sum , result);
        }

        [TestMethod]
        public void When_greater_than_minSumForDiscount_but_lower_minForDiscountCoefficient()
        {
            //arrange
            List<ItemEntity> items = new List<ItemEntity>();
            items.Add(
                      new ItemEntity
                      {
                          ItemId = Guid.NewGuid(),
                          Name = "carrot",
                          Description = "delicious",
                          Price = (decimal)5.5,
                          Count = 300
                      }
                     );

            DiscountPolicy discount = new DiscountPolicy();
            decimal sum = items.Sum<ItemEntity>(x => x.Price * x.Count) * (decimal)0.3;

            //act
            decimal result = discount.PriceCalculation(items);

            //assert
            Assert.AreEqual(sum, result);
        }

        [TestMethod]
        public void When_greater_than_minSumForDiscount_and_minForDiscountCoefficient()
        {
            //arrange
            List<ItemEntity> items = new List<ItemEntity>();
            items.Add(
                      new ItemEntity
                      {
                          ItemId = Guid.NewGuid(),
                          Name = "carrot",
                          Description = "delicious",
                          Price = (decimal)5.5,
                          Count = 300
                      }
                     );
            items.Add(
                      new ItemEntity
                      {
                          ItemId = Guid.NewGuid(),
                          Name = "tomatoes",
                          Description = "delicious",
                          Price = (decimal)10,
                          Count = 10
                      }
                     );
                      
            items.Add(
                      new ItemEntity
                      {
                          ItemId = Guid.NewGuid(),
                          Name = "green onion",
                          Description = "delicious",
                          Price = (decimal)5,
                          Count = 100
                      }
                     );

            DiscountPolicy discount = new DiscountPolicy(30,2);
            decimal sum = items.Sum<ItemEntity>(x => x.Price * x.Count) * (decimal)0.3 * (decimal)1.67;

            //act
            decimal result = discount.PriceCalculation(items);

            //assert
            Assert.AreEqual(sum, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void When_null_argument_come()
        {
            //arrange
            List<ItemEntity> items = null;

            DiscountPolicy discount = new DiscountPolicy(30, 2);           
            //act
            decimal result = discount.PriceCalculation(items);

        }
    }
}
