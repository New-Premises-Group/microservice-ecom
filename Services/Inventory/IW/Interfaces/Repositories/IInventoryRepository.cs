using IW.Models.Entities;

namespace IW.Interfaces
{
    public interface IInventoryRepository:IBaseRepository<Inventory>
    {
        /// <summary>
        /// Update inventory quantity. Get stored quantity minus 
        /// passed in quantity
        /// </summary>
        /// <param name="inventory"></param>
        void UpdateQuantity(Inventory inventory);
        void GetId(out int id, Inventory inventory);
    }
}
