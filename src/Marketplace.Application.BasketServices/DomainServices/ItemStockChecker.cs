using System;
using System.Threading.Tasks;
using Marketplace.Baskets.Rules;

namespace Marketplace.Baskets.DomainServices
{
    public class ItemStockChecker : IItemStockChecker
    {
        public async Task<bool> IsAvaliable(int itemId, int quantity)
        {
            // Urun stok ve satis durum bilgisi icin ilgili endpointe yada sinifa istek yapilir
            // Urun stok bilgisi ile sepete eklenmek istenen miktar karsilastirilir
            // Elde edilen sonuca gore geriye true/false donulur
            
            return true;
        }
    }
}