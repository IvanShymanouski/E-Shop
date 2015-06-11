using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace E_ShopDomainModel
{
    [System.Serializable]
    public class ChangeStatusExeption : Exception
    {
        public ChangeStatusExeption() { }

        public ChangeStatusExeption(string message) : base(message) { }

        public ChangeStatusExeption(string message, Exception inner) : base(message, inner) { }

        protected ChangeStatusExeption(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }

}
