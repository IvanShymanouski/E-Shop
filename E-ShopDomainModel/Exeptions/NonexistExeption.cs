using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace E_ShopDomainModel
{
    [System.Serializable]
    public class NonexistExeption : Exception
    {
        public NonexistExeption() { }

        public NonexistExeption(string message) : base(message) { }

        public NonexistExeption(string message, Exception inner) : base(message, inner) { }

        protected NonexistExeption(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }

}
