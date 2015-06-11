using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_ShopDomainModel.Interfaces;

namespace E_ShopDomainModel
{
    public class ItemService : INamedServices<ItemEntity>
    {
        /*IRepository _repository;

        Item(IRepository repository)
        {
            _repository = repository;
        }*/

        public IEnumerable<ItemEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ItemEntity> GetById(Guid Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ItemEntity> GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public void Create(ItemEntity entity)
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

        public void Update(ItemEntity entity)
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

        public void Delete(ItemEntity entity)
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
