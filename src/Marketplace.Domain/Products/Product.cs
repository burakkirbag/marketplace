using Marketplace.Domain;
using Marketplace.Domain.Entities;
using Marketplace.Products.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Marketplace.Products
{
    public class Product : EntityBase, IAggregateRoot
    {
        #region [props]

        public virtual string Name { get; private set; }

        public virtual string Sku { get; private set; }

        public virtual string Barcode { get; private set; }

        public virtual decimal Price { get; private set; }

        public virtual int Stock { get; private set; }

        private List<ProductImage> _images = new List<ProductImage>();
        public IReadOnlyCollection<ProductImage> Images => _images?.AsReadOnly();
        #endregion

        #region [ctor]
        public Product()
        {
        }

        private Product(string name,
            string barcode,
            decimal price,
            int stock,
            List<ProductImage> images)
        {
            Name = name;
            Barcode = barcode;
            Sku = Guid.NewGuid().ToString();
            Price = price;
            Stock = stock;

            if (_images == null) _images = new List<ProductImage>();

            if (images.Any()) images.ForEach(image => AddImage(image));

            AddEvent(new ProductCreatedEvent(this));

        }
        #endregion

        public static Product Create(string name,
            string barcode,
            decimal price,
            int stock,
            List<ProductImage> images)
        {
            var product = new Product(name, barcode, price, stock, images);

            return product;
        }

        public Product AddImage(ProductImage image)
        {
            if (_images == null) _images = new List<ProductImage>();

            _images.Add(image);

            return this;
        }
    }
}
