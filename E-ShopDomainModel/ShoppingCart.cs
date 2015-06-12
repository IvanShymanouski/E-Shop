using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_ShopDomainModel.Interfaces;

namespace E_ShopDomainModel
{
    public class ShoppingCart : IShoppingCart
    {
        #region fields
        public ItemEntity this[int i]
        {
            get 
            {
                if (i >= items.Count) i = items.Count - 1;
                else if (i < 0) i = 0;
                return items.ElementAt(i);
            }
            private set { }
        }
        public int Length
        {
            get { return items.Count; }
            private set { }
        }
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

        public ShoppingCart(IDiscountPolicy discount, ShoppingCartEntity entity)
        {
            Id = entity.Id;
            State = entity.State;
            _discount = discount;
        }

        public void AddItemToCart(ItemEntity entity)
        {
            if (entity == null) throw new ArgumentNullException("Entity object is null");
            foreach (var item in items)
            {
                if (item.ItemId == entity.ItemId)
                {
                    throw new AlreadyExistExeption("Adding item in Cart " + entity.ItemId + " faild");
                }
            }
            items.Add(entity);
            
        }

        public void DeleteItemFromCart(Guid itemId)
        {
            if (itemId == null) throw new ArgumentNullException("Guid object is null");
            foreach (var item in items)
            {
                if (item.ItemId == itemId)
                {
                    items.Remove(item);
                    return;
                }
            }
            throw new NonexistExeption("Deleting item in Cart " + itemId + " faild");
        }

        public void MakePurchase(IPayment payment, IServices<ShoppingCartEntity> repository)
        {
            if (items.Count == 0) throw new NonexistExeption("Shopping cart is empty");
            if (State == 0)
            {                
                try
                {
                    decimal sum = _discount.PriceCalculation(items);
                    payment.MakePayment(sum);
                    State++;
                    repository.Create(new ShoppingCartEntity { Id = this.Id, State = this.State });                    
                }
                catch (Exception ex)
                {
                    throw new TransactionFaildExeption("External \"MakePurchase\" transaction exeption", ex);
                }
            }
            else throw new TransactionFaildExeption("Attempt double accepting");
        }

        public void ChangeState(long step, IServices<ShoppingCartEntity> repository)
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
                try
                {
                    repository.Update(new ShoppingCartEntity { Id = this.Id, State = this.State });
                }
                catch (Exception ex)
                {
                    throw new TransactionFaildExeption("External \"ChangeState\" transaction exeption", ex);
                }
            }
            else throw new ChangeStatusExeption("Attempt change status befor accepting order");
        }
    }
        
}
