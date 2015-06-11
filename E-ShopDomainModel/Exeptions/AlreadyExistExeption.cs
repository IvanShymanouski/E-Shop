using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace E_ShopDomainModel
{
    [System.Serializable]
    class AlreadyExistExeption : Exception
    {
        public AlreadyExistExeption() { }

        public AlreadyExistExeption(string message) : base(message) { }

        public AlreadyExistExeption(string message, Exception inner) : base(message, inner) { }

        protected AlreadyExistExeption(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }

}
