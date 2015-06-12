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
    public class DemonstrationTests
    {
        [TestMethod]
        public void Select_all_items_in_some_category_and_their_purchase()
        {
            //arrange
            IDiscountPolicy stubDiscount;
            INamedServices<ItemEntity> stubItemRepo;            
            IItemCategoryRService stubItemCatRepo;
            INamedServices<CategoryEntity> stubCategoryRepo;
            IServices<ShoppingCartEntity> mockShopCartRepo;
            IPayment mockPayment;

            InitializeStubsAndMocks(out stubDiscount,
                                    out stubItemRepo,
                                    out stubItemCatRepo,
                                    out stubCategoryRepo,
                                    out mockShopCartRepo,
                                    out mockPayment
                                   );

            var shoppingCart = new ShoppingCart(stubDiscount);

            //act
            //**get categories
            var categories = (List<CategoryEntity>)stubCategoryRepo.GetAll();

            //**suppose fisrt category selected by user
            var selected = stubItemCatRepo.GetByCategoryId(categories[0].Id);

            //**maybe each item in this category was ordered by user
            foreach (var sel in selected)
            {
                ItemEntity entity = stubItemRepo.GetById(sel.ItemId).First();
                shoppingCart.AddItemToCart(entity);
            }

            shoppingCart.MakePurchase(mockPayment,mockShopCartRepo);

            //**some steps of delivery have been passed
            shoppingCart.ChangeState(2,mockShopCartRepo);

            //assert
            Assert.AreEqual(shoppingCart.Length, 2);
            Assert.AreEqual(shoppingCart.State, 3);
            mockShopCartRepo.VerifyAllExpectations();
            mockPayment.VerifyAllExpectations();

        }

        private void InitializeStubsAndMocks
            (
             out IDiscountPolicy stubDiscount,
             out INamedServices<ItemEntity> stubItemRepo,
             out IItemCategoryRService stubItemCatRepo,
             out INamedServices<CategoryEntity> stubCategoryRepo,
             out IServices<ShoppingCartEntity> mockShopCartRepo,
             out IPayment mockPayment
            )
        {
            stubDiscount = MockRepository.GenerateStub<IDiscountPolicy>();
            stubItemRepo = MockRepository.GenerateStub<INamedServices<ItemEntity>>();
            stubItemCatRepo = MockRepository.GenerateStub<IItemCategoryRService>();
            stubCategoryRepo = MockRepository.GenerateStub<INamedServices<CategoryEntity>>();
            mockShopCartRepo = MockRepository.GenerateMock<IServices<ShoppingCartEntity>>();
            mockPayment = MockRepository.GenerateMock<IPayment>();

            List<ItemEntity> itemList = GenerateTenItems();

            foreach (var item in itemList)
            {
                List<ItemEntity> ItemList = new List<ItemEntity>();
                ItemList.Add(item);
                stubItemRepo.Stub(x => x.GetById(item.ItemId)).Return(ItemList);
            }

            List<CategoryEntity> categoryList = GenerateFiveCategory();
            stubCategoryRepo.Stub(x => x.GetAll()).Return(categoryList);

            List<ItemCategoryRelashionshipEntity> itemCatEntites = GenerateItemCatEntites(itemList, categoryList);
            var selectedRelationships = itemCatEntites.Where(x => x.CategoryId == categoryList[0].Id);
            stubItemCatRepo.Stub(x => x.GetByCategoryId(categoryList[0].Id)).Return(selectedRelationships);

            stubDiscount.Stub(x => x.PriceCalculation(new List<ItemEntity>())).IgnoreArguments().Return((decimal)35);
            mockShopCartRepo.Expect(x => x.Create(new ShoppingCartEntity { })).IgnoreArguments();
            mockShopCartRepo.Expect(x => x.Update(new ShoppingCartEntity { })).IgnoreArguments();
            mockPayment.Expect(x => x.MakePayment((decimal)35));
        }

        private List<ItemEntity> GenerateTenItems()
        {
            List<ItemEntity> list = new List<ItemEntity>();
            for (int i = 0; i < 10; i++)
            {
                list.Add(new ItemEntity
                {
                    ItemId = Guid.NewGuid(),
                    Description = "just super",
                    Name = (1000 + i).ToString(),
                    Count = i+1,
                    Price = i+25
                }
                    );
            }
            return list;
        }

        private List<CategoryEntity> GenerateFiveCategory()
        { 
            List<CategoryEntity> list = new List<CategoryEntity>();
            for (int i = 0; i < 5; i++)
            {
                list.Add(new CategoryEntity
                        {
                            Id = Guid.NewGuid(),
                            Description = "just super",
                            Name = (2000+i).ToString()
                        }
                    );
            }
            return list;
        }

        private List<ItemCategoryRelashionshipEntity> GenerateItemCatEntites
            (
              List<ItemEntity> ItemList,
              List<CategoryEntity> CategoryList
            )
        {
            List<ItemCategoryRelashionshipEntity> list = new List<ItemCategoryRelashionshipEntity>();
            int j = 0;
            for (int i = 0; i < ItemList.Count; i++)
            {
                list.Add(new ItemCategoryRelashionshipEntity
                         {
                             ItemId = ItemList[i].ItemId,
                             CategoryId = CategoryList[j].Id
                         }
                        );
                if (i % 2 == 1) j++;
            }
            return list;
        }
    }
}
