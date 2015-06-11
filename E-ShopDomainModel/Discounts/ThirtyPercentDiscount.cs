using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_ShopDomainModel.Interfaces;

namespace E_ShopDomainModel
{
    public class ThirtyPercentDiscount : IDiscount<ItemEntity> //staic maybe
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

        private string name = "Thirty percent dicount";
        #endregion

        public decimal GetDiscount(ItemEntity entity)  
        {
            return entity.Price * (decimal)0.3;
        }
    }
}
