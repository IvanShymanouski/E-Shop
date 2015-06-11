using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_ShopDomainModel.Interfaces;

namespace E_ShopDomainModel
{
    class ShoppingCartService : IServices<ShoppingCartEntity>
    {
        /*IRepository _repository;

        ShoppingCart(IRepository repository)
        {
            _repository = repository;
        }*/

        public IEnumerable<ShoppingCartEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ShoppingCartEntity> GetById(Guid Id)
        {
            throw new NotImplementedException();
        }

        public void Create(ShoppingCartEntity entity)
        {
            throw new NotImplementedException();
            /*try
            {
                _repository.Create(entity);
            }
            catch (Exception ex)
            {
                throw new TransactionFaildExeption("Externel \"Create\" exeption", ex);
            }*/
        }

        public void Update(ShoppingCartEntity entity)
        {
            throw new NotImplementedException();
            /*
            try
            {
                _repository.Update(entity);
            }
            catch (Exception ex)
            {
                throw new TransactionFaildExeption("Externel \"Update\" exeption", ex);
            }*/
        }

        public void Delete(ShoppingCartEntity entity)
        {
            throw new NotImplementedException();
            /*
            try
            {
                _repository.Delete(entity);
            }
            catch (Exception ex)
            {
                throw new TransactionFaildExeption("Externel \"Delete\" exeption", ex);
            }*/
        }
    }
}
