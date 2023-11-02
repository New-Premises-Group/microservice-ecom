using IW.Models.DTOs;
using IW.Models.DTOs.Product;
using IW.Models.DTOs.ProductDto;

namespace IW.Interfaces
{
    public interface IProductService
    {
        Task CreateProduct(CreateProduct input);
        Task<ProductDto?> GetProduct(int id);
        Task<IEnumerable<ProductDto>> GetProducts(int offset, int amount);
        Task<IEnumerable<ProductDto>> GetProducts(GetProduct query, int offset , int amount );
        Task UpdateProduct(int id, UpdateProduct model);
        Task DeleteProduct(int id);
    }
}
