using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_ShopDomainModel.Interfaces;

namespace E_ShopDomainModel
{
    class FiftyPercentDiscount : IDiscount<IItemEntity>
    {
        #region fields
        public string Name
        {
            get
            {
                return name;
            }
            private set
            {
                name = value;
            }
        }

        private string name = "Fifty percent dicount";
        #endregion

        public decimal GetDiscount(IItemEntity entity)
        {
            return entity.Price * (decimal)0.5;
        }
    }
}
