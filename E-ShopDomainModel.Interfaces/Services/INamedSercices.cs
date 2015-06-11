using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ShopDomainModel.Interfaces
{
    public interface INamedServices<TEntity> : IServices<TEntity>
           where TEntity : class, IEntity
    {
        IEnumerable<TEntity> GetByName(string name);
    }
}
