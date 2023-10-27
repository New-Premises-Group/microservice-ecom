using IW.Models.Entities;

namespace IW.Interfaces
{
    public interface IInventoryRepository:IBaseRepository<Inventory>
    {
        void UpdateQuantity(Inventory inventory);
        void GetId(out int id, Inventory inventory);
    }
}
