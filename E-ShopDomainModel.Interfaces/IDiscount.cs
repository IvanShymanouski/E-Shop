using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ShopDomainModel.Interfaces
{
    public interface IDiscount<TEntity> where TEntity: class, IEntity
    {
        string Name { get; }

        decimal GetDiscount(TEntity entity);
    }
}