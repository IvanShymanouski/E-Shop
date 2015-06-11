using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ShopDomainModel.Interfaces
{
    public sealed class OrderState
    {
        public string this[long i]
        {
            get
            {
                if (i < 0) i = 0;
                else if (i >= state.Length) i = state.Length - 1;

                return state[i];
            }
            private set { }
        }

        private string[] state = { "Not formed", "Ordered", "In progress", "Deliverd" };
    }
}
