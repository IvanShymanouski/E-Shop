using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ShopDomainModel.Interfaces
{
    public class ItemCategoryRelashionshipEntity : IEntity
    {
        public Guid CategoryId { get; set; }
        public Guid ItemId { get; set; }
    }
}
