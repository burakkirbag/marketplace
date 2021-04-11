using Marketplace.Domain.Values;
using System.Collections.Generic;

namespace Marketplace.Products
{
    public class ProductImage : ValueObjectBase
    {
        public string Name { get; private set; }

        public string Url { get; private set; }

        public ProductImage(string name, string url)
        {
            Name = name;
            Url = url;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
            yield return Url;
        }
    }
}
