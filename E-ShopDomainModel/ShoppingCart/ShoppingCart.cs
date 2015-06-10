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
        public Guid Id
        {
            get { return order.Id; }
            private set { order.Id = value; }
        }
        public string Status { get { return states[order.State]; } }

        private IShoppingCartEntity order;
        private IDiscountPolicy _discount;
        private OrderState states = new OrderState();
        private List<IItemEntity> items = new List<IItemEntity>();
        #endregion

        public ShoppingCart(IShoppingCartEntity entity, IDiscountPolicy discount)
        {
            _discount = discount;
            order = entity;
            order.State = 0;
            Id = Guid.NewGuid();
            while (Id == Guid.Empty) Id = Guid.NewGuid();
        }

        public ShoppingCart(IShoppingCartEntity entity, IDiscountPolicy discount, Guid id) : this(entity,discount)
        {
            Id = id;
        }

        public void AddItemToCart(Guid itemId, IRepository<IItemEntity> repository)
        {
            foreach (var item in items)
            {
                if (item.ItemId == itemId)
                {
                    throw new AlreadyExistExeption("Adding item in Cart "+order.Id+" faild");
                }
            }
            try
            {
                items.Add((IItemEntity)repository.GetById(itemId));
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
            if (order.State == 0)
            {                
                try
                {
                    decimal sum = _discount.PriceCalculation(items);
                    payment.MakePayment(sum);
                    order.State++;
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
            if (order.State != 0)
            {
                long temp = order.State;
                try
                {
                    checked
                    {
                        order.State += step;
                    }
                    if (order.State < 0) throw new OverflowException("Negative value by decrementing");
                    else if (order.State == 0) throw new OverflowException("Attempt to reset payment");
                }
                catch (OverflowException ex)
                {
                    order.State = temp;
                    throw new ChangeStatusExeption("Overflow index exeption", ex);
                }
            }
            else throw new ChangeStatusExeption("Attempt change status befor accepting order");
        }

        public void CreateOrder(IRepository<IShoppingCartEntity> repository)
        {
            try
            {
                repository.Create(order);
            }
            catch (Exception ex)
            {
                throw new TransactionFaildExeption("Externel \"CreateOrder\" exeption", ex);
            }
        }

        public void CreateUpdate(IRepository<IShoppingCartEntity> repository)
        {
            try
            {
                repository.Update(order);
            }
            catch (Exception ex)
            {
                throw new TransactionFaildExeption("Externel \"UpdateOrder\" exeption", ex);
            }
        }

        public void DeleteOrder(IRepository<IShoppingCartEntity> repository)
        {
            try
            {
                repository.Delete(order);
                Id = Guid.NewGuid();
                order.State = 0;
            }
            catch (Exception ex)
            {
                throw new TransactionFaildExeption("Externel \"DeleteOrder\" exeption", ex);
            }
        }
    }
        
}
