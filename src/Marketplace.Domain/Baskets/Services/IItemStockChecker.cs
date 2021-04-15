using System;
using System.Threading.Tasks;
using Marketplace.Domain.Services;

namespace Marketplace.Baskets.Rules
{
    public interface IItemStockChecker : IDomainService
    {
        Task<bool> IsAvaliable(int itemId, int quantity);
    }
}