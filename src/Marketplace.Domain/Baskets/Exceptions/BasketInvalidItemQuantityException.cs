using System;
using System.Runtime.Serialization;
using Marketplace.Domain;

namespace Marketplace.Baskets.Exceptions
{
    [Serializable]
    public class BasketInvalidItemQuantityException : DomainException
    {
        public BasketInvalidItemQuantityException()
        {
        }

        public BasketInvalidItemQuantityException(string message) : base(message)
        {
        }

        public BasketInvalidItemQuantityException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public BasketInvalidItemQuantityException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}