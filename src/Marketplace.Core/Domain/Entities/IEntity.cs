using Marketplace.Domain.Events;
using System.Collections.Generic;
using MongoDB.Bson;

namespace Marketplace.Domain.Entities
{
    public interface IEntity
    {
        public string Id { get; set; }

        public bool HasEvents();

        public IEnumerable<IDomainEvent> GetEvents();

        public void ClearEvents();
    }
}