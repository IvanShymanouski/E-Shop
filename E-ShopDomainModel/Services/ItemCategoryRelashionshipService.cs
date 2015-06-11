using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_ShopDomainModel.Interfaces;

namespace E_ShopDomainModel
{
    public class ItemCategoryRelashionshipService : IItemCategoryRService //translation by mapping
    {
        /*IRepository _repository;

        ItemCategoryRelashionship(IRepository repository)
        {
            _repository = repository;
        }*/

        public IEnumerable<ItemCategoryRelashionshipEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ItemCategoryRelashionshipEntity> GetById(Guid Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ItemCategoryRelashionshipEntity> GetByCategoryId(Guid Id)
        {
            throw new NotImplementedException();
        }

        public void Create(ItemCategoryRelashionshipEntity entity)
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

        public void Update(ItemCategoryRelashionshipEntity entity)
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

        public void Delete(ItemCategoryRelashionshipEntity entity)
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
