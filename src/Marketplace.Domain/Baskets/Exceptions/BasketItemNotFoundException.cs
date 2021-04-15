using System;
using System.Runtime.Serialization;
using Marketplace.Domain;

namespace Marketplace.Baskets.Exceptions
{
    [Serializable]
    public class BasketItemNotFoundException: DomainException
    {
        public BasketItemNotFoundException()
        {
        }

        public BasketItemNotFoundException(string message) : base(message)
        {
        }

        public BasketItemNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public BasketItemNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}