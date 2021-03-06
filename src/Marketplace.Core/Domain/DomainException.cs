using System;
using System.Runtime.Serialization;

namespace Marketplace.Domain
{
    [Serializable]
    public abstract class DomainException : MarketplaceException
    {
        protected DomainException()
        {

        }

        protected DomainException(string message) : base(message)
        {

        }

        protected DomainException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        protected DomainException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
