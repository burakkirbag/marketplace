using Marketplace.Domain;
using System;
using System.Runtime.Serialization;

namespace Marketplace.Products.Exceptions
{
    public class ProductImageNotFoundException : DomainException
    {
        public ProductImageNotFoundException()
        {
        }

        public ProductImageNotFoundException(string message) : base(message)
        {
        }

        public ProductImageNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public ProductImageNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
