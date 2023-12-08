using IW.Models.DTOs;
using IW.Models.DTOs.Product;

namespace IW.Interfaces
{
    public interface IProductService
    {
        Task CreateProduct(CreateProduct input);
        Task<ProductDto?> GetProduct(int id);
        Task<IEnumerable<ProductDto>> GetProducts(int page, int amount);
        Task<IEnumerable<ProductDto>> GetProducts(GetProduct query, int page , int amount );
        Task<IEnumerable<ProductDto>> GetProductsByName(string name, int page, int amount);
        Task UpdateProduct(int id, UpdateProduct model);
        Task DeleteProduct(int id);
    }
}
