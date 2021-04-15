using System;
using System.Runtime.Serialization;

namespace Marketplace.Application
{
    [Serializable]
    public abstract class ApplicationException : MarketplaceException
    {
        protected ApplicationException()
        {
        }

        protected ApplicationException(string message) : base(message)
        {
        }

        protected ApplicationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        protected ApplicationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}