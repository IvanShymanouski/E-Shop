using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using Rhino.Mocks.Exceptions;
using System.Collections.Generic;
using System.Linq;
using E_ShopDomainModel.Interfaces;

namespace E_ShopDomainModel.Tests
{
    [TestClass]
    public class ShoppingCartTests
    {
        [TestMethod]
        public void Creation_new_cart()
        {
            //arrange
            var stubDiscount = MockRepository.GenerateStub<IDiscountPolicy>();
            ShoppingCartEntity entity = new ShoppingCartEntity
            {
                Id = Guid.NewGuid(),
                State = 2
            };

            //act
            ShoppingCart shoppingCart = new ShoppingCart(stubDiscount, entity);            

            //assert
            Assert.AreEqual(entity.Id, shoppingCart.Id);
            Assert.AreEqual(entity.State, shoppingCart.State);
        }

        #region AddItemToCart
        [TestMethod]
        public void Adding_items_to_card()
        {
            //arrange
            var stubDiscount = MockRepository.GenerateStub<IDiscountPolicy>();
            ShoppingCart shoppingCart = new ShoppingCart(stubDiscount);

            ItemEntity item1 = new ItemEntity
            {
                ItemId = Guid.NewGuid(),
                Name = "carrot",
                Description = "delicious",
                Price = (decimal)2.5,
                Count = 5
            };
            ItemEntity item2 = new ItemEntity
            {
                ItemId = Guid.NewGuid(),
                Name = "carrot",
                Description = "delicious",
                Price = (decimal)2.5,
                Count = 5
            };
            ItemEntity item3 = new ItemEntity
            {
                ItemId = Guid.NewGuid(),
                Name = "carrot",
                Description = "delicious",
                Price = (decimal)2.5,
                Count = 5
            };

            //act
            shoppingCart.AddItemToCart(item1);
            shoppingCart.AddItemToCart(item2);
            shoppingCart.AddItemToCart(item3);

            //assert
            Assert.AreEqual(3, shoppingCart.Length, "Wrong length");
            Assert.AreEqual(shoppingCart[0], item1, "Wrong first element");
            Assert.AreEqual(shoppingCart[1], item2, "Wrong second element");
            Assert.AreEqual(shoppingCart[2], item3, "Wrong third element");
        }

        [TestMethod]
        [ExpectedException(typeof(AlreadyExistExeption))]
        public void Adding_existing_item()
        {
            //arrange
            var stubDiscount = MockRepository.GenerateStub<IDiscountPolicy>();
            ShoppingCart shoppingCart = new ShoppingCart(stubDiscount);
            ItemEntity item = new ItemEntity
                      {
                          ItemId = Guid.NewGuid(),
                          Name = "green onion",
                          Description = "delicious",
                          Price = (decimal)5,
                          Count = 100
                      };

            shoppingCart.AddItemToCart(item);

            //act
            shoppingCart.AddItemToCart(item);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void When_try_adding_null_items_throw_exeption()
        {
            //arrange
            var stubDiscount = MockRepository.GenerateStub<IDiscountPolicy>();
            ShoppingCart shoppingCart = new ShoppingCart(stubDiscount);

            //act
            shoppingCart.AddItemToCart(null);

        }
        #endregion

        #region DeleteItemFromCart
        [TestMethod]
        public void Deleting_items_from_card()
        {
            //arrange
            var stubDiscount = MockRepository.GenerateStub<IDiscountPolicy>();
            ShoppingCart shoppingCart = new ShoppingCart(stubDiscount);

            ItemEntity item1 = new ItemEntity
            {
                ItemId = Guid.NewGuid(),
                Name = "carrot",
                Description = "delicious",
                Price = (decimal)2.5,
                Count = 5
            };
            ItemEntity item2 = new ItemEntity
            {
                ItemId = Guid.NewGuid(),
                Name = "carrot",
                Description = "delicious",
                Price = (decimal)2.5,
                Count = 5
            };
            ItemEntity item3 = new ItemEntity
            {
                ItemId = Guid.NewGuid(),
                Name = "carrot",
                Description = "delicious",
                Price = (decimal)2.5,
                Count = 5
            };

            shoppingCart.AddItemToCart(item1);
            shoppingCart.AddItemToCart(item2);
            shoppingCart.AddItemToCart(item3);

            //act
            shoppingCart.DeleteItemFromCart(item2.ItemId);
            shoppingCart.DeleteItemFromCart(item1.ItemId);

            //assert
            Assert.AreEqual(1, shoppingCart.Length, "wrong remaining element");
            Assert.AreEqual(shoppingCart[0], item3, "Wrong  element");
        }

        [TestMethod]
        [ExpectedException(typeof(NonexistExeption))]
        public void When_removing_nonexistent_element_is_ignoring()
        {
            //arrange
            var stubDiscount = MockRepository.GenerateStub<IDiscountPolicy>();
            ShoppingCart shoppingCart = new ShoppingCart(stubDiscount);

            ItemEntity item1 = new ItemEntity
            {
                ItemId = Guid.NewGuid(),
                Name = "carrot",
                Description = "delicious",
                Price = (decimal)2.5,
                Count = 5
            };
            ItemEntity item2 = new ItemEntity
            {
                ItemId = Guid.NewGuid(),
                Name = "carrot",
                Description = "delicious",
                Price = (decimal)2.5,
                Count = 5
            };

            shoppingCart.AddItemToCart(item1);
            var len = shoppingCart.Length;

            //act
            shoppingCart.DeleteItemFromCart(item2.ItemId);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void When_try_deleting_null_items_throw_exeption()
        {
            //arrange
            var stubDiscount = MockRepository.GenerateStub<IDiscountPolicy>();
            ShoppingCart shoppingCart = new ShoppingCart(stubDiscount);

            //act
            shoppingCart.AddItemToCart(null);

        }
        #endregion

        #region MakePurchase
        [TestMethod]
        [ExpectedException(typeof(NonexistExeption))]
        public void when_try_purchase_with_empty_cart()
        {
            //arrange
            var stubDiscount = MockRepository.GenerateStub<IDiscountPolicy>();
            var stubPayment = MockRepository.GenerateStub<IPayment>();
            var stubShopCartRepo = MockRepository.GenerateStub<IServices<ShoppingCartEntity>>();
            ShoppingCart shoppingCart = new ShoppingCart(stubDiscount);

            //act
            shoppingCart.MakePurchase(stubPayment,stubShopCartRepo);

        }

        [TestMethod]
        [ExpectedException(typeof(TransactionFaildExeption))]
        public void when_try_double_purchase()
        {
            //arrange
            var stubDiscount = MockRepository.GenerateStub<IDiscountPolicy>();
            var stubPayment = MockRepository.GenerateStub<IPayment>();
            var stubShopCartRepo = MockRepository.GenerateStub<IServices<ShoppingCartEntity>>();
            ShoppingCartEntity shEntity = new ShoppingCartEntity
            {
                Id = Guid.NewGuid(),
                State = 2
            };
            ShoppingCart shoppingCart = new ShoppingCart(stubDiscount, shEntity);
            ItemEntity item1 = new ItemEntity
            {
                ItemId = Guid.NewGuid(),
                Name = "carrot",
                Description = "delicious",
                Price = (decimal)2.5,
                Count = 5
            };
            shoppingCart.AddItemToCart(item1);

            //act
            shoppingCart.MakePurchase(stubPayment, stubShopCartRepo);

        }

        [TestMethod]
        public void Make_purchase_when_transaction_OK_and_cart_not_empty()
        {
            //arrange
            var stubDiscount = MockRepository.GenerateStub<IDiscountPolicy>();
            var mockPayment = MockRepository.GenerateMock<IPayment>();
            var mockShopCartRepo = MockRepository.GenerateMock<IServices<ShoppingCartEntity>>();
            ShoppingCart shoppingCart = new ShoppingCart(stubDiscount);
            ItemEntity item1 = new ItemEntity
            {
                ItemId = Guid.NewGuid(),
                Name = "carrot",
                Description = "delicious",
                Price = (decimal)2.5,
                Count = 5
            };
            shoppingCart.AddItemToCart(item1);

            var list = new List<ItemEntity>();
            list.Add(item1);

            stubDiscount.Stub(x => x.PriceCalculation(list)).Return((decimal)12.5);
            mockPayment.Expect(x => x.MakePayment((decimal)(item1.Price * item1.Count)));
            mockShopCartRepo.Expect(x => x.Create(new ShoppingCartEntity { Id = shoppingCart.Id, State = 1 })).IgnoreArguments();

            //act
            shoppingCart.MakePurchase(mockPayment, mockShopCartRepo);
            
            //assert
            Assert.AreEqual(shoppingCart.State, 1, "Wrong state after purchase");
            mockPayment.VerifyAllExpectations();
            mockShopCartRepo.VerifyAllExpectations();
        }

        [TestMethod]
        [ExpectedException(typeof(TransactionFaildExeption))]
        public void When_payment_faild_with_exeption()
        {
            //arrange
            var stubDiscount = MockRepository.GenerateStub<IDiscountPolicy>();
            var mockPayment = MockRepository.GenerateStrictMock<IPayment>();
            var stubShopCartRepo = MockRepository.GenerateStub<IServices<ShoppingCartEntity>>();
            ShoppingCart shoppingCart = new ShoppingCart(stubDiscount);
            ItemEntity item1 = new ItemEntity
            {
                ItemId = Guid.NewGuid(),
                Name = "carrot",
                Description = "delicious",
                Price = (decimal)2.5,
                Count = 5
            };
            shoppingCart.AddItemToCart(item1);

            var list = new List<ItemEntity>();
            list.Add(item1);

            stubDiscount.Stub(x => x.PriceCalculation(list)).Return((decimal)12.5);

            //act
            shoppingCart.MakePurchase(mockPayment, stubShopCartRepo);
        }

        [TestMethod]
        [ExpectedException(typeof(TransactionFaildExeption))]
        public void When_discount_faild_with_exeption()
        {
            //arrange
            var stubDiscount = MockRepository.GenerateStrictMock<IDiscountPolicy>();
            var mockPayment = MockRepository.GenerateStub<IPayment>();
            var stubShopCartRepo = MockRepository.GenerateStub<IServices<ShoppingCartEntity>>();
            ShoppingCart shoppingCart = new ShoppingCart(stubDiscount);
            ItemEntity item1 = new ItemEntity
            {
                ItemId = Guid.NewGuid(),
                Name = "carrot",
                Description = "delicious",
                Price = (decimal)2.5,
                Count = 5
            };
            shoppingCart.AddItemToCart(item1);

            //act
            shoppingCart.MakePurchase(mockPayment, stubShopCartRepo);
        }
        #endregion

        #region ChangeState
        [TestMethod]
        [ExpectedException(typeof(ChangeStatusExeption))]
        public void when_try_change_state_befor_payment()
        {
            //arrange
            var stubDiscount = MockRepository.GenerateStub<IDiscountPolicy>();
            ShoppingCart shoppingCart = new ShoppingCart(stubDiscount);
            var stubShopCartRepo = MockRepository.GenerateStub<IServices<ShoppingCartEntity>>();

            //act
            shoppingCart.ChangeState(1,stubShopCartRepo);

        }

        [TestMethod]
        [ExpectedException(typeof(ChangeStatusExeption))]
        public void owerflow_when_adding_state()
        {
            //arrange
            var stubDiscount = MockRepository.GenerateStub<IDiscountPolicy>();
            var stubShopCartRepo = MockRepository.GenerateStub<IServices<ShoppingCartEntity>>();
            ShoppingCartEntity shEntity = new ShoppingCartEntity
            {
                Id = Guid.NewGuid(),
                State = 2
            };
            ShoppingCart shoppingCart = new ShoppingCart(stubDiscount, shEntity);
            ItemEntity item1 = new ItemEntity
            {
                ItemId = Guid.NewGuid(),
                Name = "carrot",
                Description = "delicious",
                Price = (decimal)2.5,
                Count = 5
            };
            shoppingCart.AddItemToCart(item1);

            //act
            shoppingCart.ChangeState(long.MaxValue,stubShopCartRepo);

        }

        [TestMethod]
        [ExpectedException(typeof(ChangeStatusExeption))]
        public void Attempt_cansel_payment()
        {
            //arrange
            var stubDiscount = MockRepository.GenerateStub<IDiscountPolicy>();
            var stubShopCartRepo = MockRepository.GenerateStub<IServices<ShoppingCartEntity>>();
            ShoppingCartEntity shEntity = new ShoppingCartEntity
            {
                Id = Guid.NewGuid(),
                State = 2
            };
            ShoppingCart shoppingCart = new ShoppingCart(stubDiscount, shEntity);
            ItemEntity item1 = new ItemEntity
            {
                ItemId = Guid.NewGuid(),
                Name = "carrot",
                Description = "delicious",
                Price = (decimal)2.5,
                Count = 5
            };
            shoppingCart.AddItemToCart(item1);

            //act
            shoppingCart.ChangeState(-2,stubShopCartRepo);

        }

        [TestMethod]
        public void Incrementing_status()
        {
            //arrange
            var stubDiscount = MockRepository.GenerateStub<IDiscountPolicy>();
            var mockShopCartRepo = MockRepository.GenerateMock<IServices<ShoppingCartEntity>>();
            ShoppingCartEntity shEntity = new ShoppingCartEntity
            {
                Id = Guid.NewGuid(),
                State = 2
            };
            ShoppingCart shoppingCart = new ShoppingCart(stubDiscount, shEntity);
            ItemEntity item1 = new ItemEntity
            {
                ItemId = Guid.NewGuid(),
                Name = "carrot",
                Description = "delicious",
                Price = (decimal)2.5,
                Count = 5
            };
            shoppingCart.AddItemToCart(item1);
            mockShopCartRepo.Expect(x => x.Update(new ShoppingCartEntity { Id = shoppingCart.Id, State = 4 })).IgnoreArguments();

            //act
            shoppingCart.ChangeState(2,mockShopCartRepo);

            //assert
            Assert.AreEqual(4,shoppingCart.State,"Wrong adding state (+2 expected)");
            mockShopCartRepo.VerifyAllExpectations();
        }

        [TestMethod]
        [ExpectedException(typeof(TransactionFaildExeption))]
        public void When_try_update_and_exeption_occur()
        {
            //arrange
            var stubDiscount = MockRepository.GenerateStub<IDiscountPolicy>();
            var mockShopCartRepo = MockRepository.GenerateStrictMock<IServices<ShoppingCartEntity>>();
            ShoppingCartEntity shEntity = new ShoppingCartEntity
            {
                Id = Guid.NewGuid(),
                State = 2
            };
            ShoppingCart shoppingCart = new ShoppingCart(stubDiscount, shEntity);
            ItemEntity item1 = new ItemEntity
            {
                ItemId = Guid.NewGuid(),
                Name = "carrot",
                Description = "delicious",
                Price = (decimal)2.5,
                Count = 5
            };
            shoppingCart.AddItemToCart(item1);            

            //act
            shoppingCart.ChangeState(2, mockShopCartRepo);
        }
        #endregion
    }
}
