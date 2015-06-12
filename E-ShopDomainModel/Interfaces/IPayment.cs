using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ShopDomainModel.Interfaces
{
    public interface IPayment
    {
        void MakePayment(decimal price);
    }
}
