using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ShopDomainModel.Interfaces
{
    public interface IShoppingCartEntity
    {
        Guid Id { get; }
        // private List<IItemEntity> items;
        long State { get; }
    }

    public class ShoppingCartEntity : IEntity, IShoppingCartEntity
    {
        public Guid Id { get; set; }
        // private List<IItemEntity> items;
        public long State { get; set; }
    }
}
