using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_ShopDomainModel.Interfaces;

namespace E_ShopDomainModel
{
    class CategoryService : INamedServices<CategoryEntity> //translation by mapping
    {
        /*IRepository _repository;

        Category(IRepository repository)
        {
            _repository = repository;
        }*/

        public IEnumerable<CategoryEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CategoryEntity> GetById(Guid Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CategoryEntity> GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public void Create(CategoryEntity entity)
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

        public void Update(CategoryEntity entity)
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

        public void Delete(CategoryEntity entity)
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
