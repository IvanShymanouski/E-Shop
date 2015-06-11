﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ShopDomainModel.Interfaces
{
    public interface IShoppingCart : IShoppingCartEntity
    {
        void AddItemToCart(Guid itemId, IServices<ItemEntity> repository);
        void DeleteItemFromCart(Guid itemId);
        void MakePurchase(IPayment payment);
        void ChangeState(long step);
    }
}