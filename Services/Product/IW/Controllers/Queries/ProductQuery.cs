using IW.Common;
using IW.Interfaces;
using IW.Models.DTOs.Product;

namespace IW.Controllers.Queries
{
    [ExtendObjectType("Query")]
    public class ProductQuery
    {
        public async Task<ProductDto> GetProduct(int id, [Service] IProductService productService)
        {
            var result = await productService.GetProduct(id);
            return result;
        }

        public ProductDto? GetProductSync(int id, [Service] IProductService productService)
        {
            var result = productService.GetProductSync(id);
            return result;
        }

        public async Task<IEnumerable<ProductDto>> GetProducts([Service] IProductService productService, int page = ((int)PAGINATING.OffsetDefault), int amount = ((int)PAGINATING.AmountDefault))
        {
            var results = await productService.GetProducts(page, amount);
            return results;
        }

        public async Task<IEnumerable<ProductDto>> GetProducts(GetProduct query, [Service] IProductService productService, int page = ((int)PAGINATING.OffsetDefault), int amount = ((int)PAGINATING.AmountDefault))
        {
            var results = await productService.GetProducts(query, page, amount);
            return results;
        }

        public async Task<IEnumerable<ProductDto>> GetProductsByName(string name, [Service] IProductService productService, int page = ((int)PAGINATING.OffsetDefault), int amount = ((int)PAGINATING.AmountDefault))
        {
            var results = await productService.GetProductsByName(name, page, amount);
            return results;
        }
    }
}
