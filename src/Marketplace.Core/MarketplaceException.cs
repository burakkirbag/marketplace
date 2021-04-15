using System;
using System.Runtime.Serialization;

namespace Marketplace
{
    public abstract class MarketplaceException : Exception
    {
        public MarketplaceException()
        {
        }

        protected MarketplaceException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public MarketplaceException(string? message) : base(message)
        {
        }

        public MarketplaceException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}