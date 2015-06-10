using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace E_ShopDomainModel
{
    [System.Serializable]
    class TransactionFaildExeption : Exception
    {
        public TransactionFaildExeption() { }

        public TransactionFaildExeption(string message) : base(message) { }

        public TransactionFaildExeption(string message, Exception inner) : base(message, inner) { }

        protected TransactionFaildExeption(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }

}
