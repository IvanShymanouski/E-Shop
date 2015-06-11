using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_ShopDomainModel.Interfaces;

namespace E_ShopDomainModel
{
    public class DiscountPolicy : IDiscountPolicy
    {
        readonly decimal _minSumForDiscount;
        readonly long _minForDiscountCoefficient;
        readonly float _discount;
        readonly float _coefficient;

        public DiscountPolicy(decimal minSumForDiscount = 1000,
                              long minForDiscountCoefficient = 10,
                              float discount = 0.3f,
                              float coefficient = 1.67f
                             )
        {
            _minSumForDiscount = minSumForDiscount;
            _minForDiscountCoefficient = minForDiscountCoefficient;
            _discount = discount;
            _coefficient = coefficient;
        }

        public decimal PriceCalculation(List<ItemEntity> items)
        {
            decimal sum;
            try
            {
               sum = items.Sum<ItemEntity>(x => x.Price * x.Count);
            }
            catch (ArgumentNullException ex)
            {
                throw new ArgumentNullException("items must not be null", ex);
            }

            if (sum > _minSumForDiscount)
            {
                return (items.Count > _minForDiscountCoefficient) ? sum * (decimal)_discount * (decimal)_coefficient 
                                                                 : sum * (decimal)_discount;
            }

            return sum;
        }
    }
}
