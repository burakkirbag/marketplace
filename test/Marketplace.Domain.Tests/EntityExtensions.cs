using System.Linq;
using FluentAssertions;
using KellermanSoftware.CompareNetObjects;
using Marketplace.Domain.Entities;
using Marketplace.Domain.Events;

namespace Marketplace.Domain.Tests
{
    public static class EntityExtensions
    {
        public static void ShouldPublishDomainEvents(this IEntity entity, params IDomainEvent[] events)
        {
            new CompareLogic().Compare(entity.GetEvents().ToArray(), events).AreEqual.Should().Be(true);
        }
    }
}