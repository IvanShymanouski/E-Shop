using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ShopDomainModel.Interfaces
{
    public interface IServices<TEntity> where TEntity : class, IEntity
    {
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> GetById(Guid Id);

        void Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
