using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_ShopDomainModel.Interfaces;

namespace E_ShopDomainModel
{
    class DiscountPolicy : IDiscountPolicy
    {
        const decimal minSumForDiscount = 1000;
        const long minForDiscountCoefficient = 10;
        const float discount = 0.3f;
        const float coefficient = 1.67f;

        public decimal PriceCalculation(List<IItemEntity> items)
        {
            decimal sum = items.Sum<IItemEntity>(x => x.Price);

            if (sum > minSumForDiscount)
            {
                return (items.Count > minForDiscountCoefficient) ? sum * (decimal)discount * (decimal)coefficient 
                                                                 : sum * (decimal)discount;
            }

            return sum;
        }
    }
}
