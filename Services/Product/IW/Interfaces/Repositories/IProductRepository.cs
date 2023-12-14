using IW.Models.Entities;

namespace IW.Interfaces
{
    public interface IProductRepository:IBaseRepository<Product>
    {
        Product? GetByIdSync(int id);
    }
}
