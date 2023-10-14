using IW.Common;
using IW.Interfaces;
using IW.Models;
using IW.Models.DTOs.Inventory;
using IW.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace IW.Repositories
{
    internal class InventoryRepository:GenericRepository<Inventory>,IInventoryRepository
    {
        public InventoryRepository(AppDbContext context):base(context)
        {
        }

        public override void Upsert(Inventory inventory)
        {
            dbSet.Entry(inventory).State=inventory.Id==0?EntityState.Added:EntityState.Modified;
        }

        public void UpdateQuantity(Inventory inventory)
        {
            dbSet
                .Where(
                    i=>i.ProductId==inventory.ProductId)
                .ExecuteUpdate(
                    i=>i.SetProperty(
                        i=>i.Quantity,
                        i=>i.Quantity-inventory.Quantity));
        }
    }
}
