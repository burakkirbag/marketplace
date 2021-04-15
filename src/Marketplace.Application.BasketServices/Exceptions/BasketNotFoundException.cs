using System;
using System.Runtime.Serialization;

namespace Marketplace.Baskets.Exceptions
{
    [Serializable]
    public class BasketNotFoundException : ApplicationException
    {
        public BasketNotFoundException()
        {
        }

        public BasketNotFoundException(string message) : base(message)
        {
        }

        public BasketNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BasketNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}