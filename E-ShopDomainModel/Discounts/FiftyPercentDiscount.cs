using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_ShopDomainModel.Interfaces;

namespace E_ShopDomainModel
{
    public class FiftyPercentDiscount : IDiscount<ItemEntity>
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

        public decimal GetDiscount(ItemEntity entity)
        {
            return entity.Price * (decimal)0.5;
        }
    }
}
