using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_ShopDomainModel.Interfaces;

namespace E_ShopDomainModel
{
    class ShoppingCart : IShoppingCart
    {
        #region fields
        public Guid Id { get; private set; }
        public long State { get; private set; }
       
        private IDiscountPolicy _discount;
        private List<ItemEntity> items = new List<ItemEntity>();
        #endregion

        public ShoppingCart(IDiscountPolicy discount)
        {
            _discount = discount;
            Id = Guid.NewGuid();
            while (Id == Guid.Empty) Id = Guid.NewGuid();
            State = 0;
        }

        public ShoppingCart(ShoppingCartEntity entity, IDiscountPolicy discount)
        {
            Id = entity.Id;
            State = entity.State;
            _discount = discount;
        }

        public void AddItemToCart(Guid itemId, IServices<ItemEntity> repository)
        {
            foreach (var item in items)
            {
                if (item.ItemId == itemId)
                {
                    throw new AlreadyExistExeption("Adding item in Cart "+Id+" faild");
                }
            }
            try
            {
                items.Add((ItemEntity)repository.GetById(itemId));
            }
            catch(Exception ex)
            {
                throw new TransactionFaildExeption("Externel transaction exeption",ex);
            }
        }

        public void DeleteItemFromCart(Guid itemId)
        {
            foreach (var item in items)
            {
                if (item.ItemId == itemId)
                {
                    items.Remove(item);
                    break;
                }
            }
        }

        public void MakePurchase(IPayment payment)
        {
            if (State == 0)
            {                
                try
                {
                    decimal sum = _discount.PriceCalculation(items);
                    payment.MakePayment(sum);
                    State++;
                }
                catch (Exception ex)
                {
                    throw new TransactionFaildExeption("External \"MakePurchase\" transaction exeption", ex);
                }
            }
            else throw new TransactionFaildExeption("Attempt double accepting");
        }

        public void ChangeState(long step)
        {
            if (State != 0)
            {
                long temp = State;
                try
                {
                    checked
                    {
                        State += step;
                    }
                    if (State < 0) throw new OverflowException("Negative value by decrementing");
                    else if (State == 0) throw new OverflowException("Attempt to reset payment");
                }
                catch (OverflowException ex)
                {
                    State = temp;
                    throw new ChangeStatusExeption("Overflow index exeption", ex);
                }
            }
            else throw new ChangeStatusExeption("Attempt change status befor accepting order");
        }
    }
        
}
