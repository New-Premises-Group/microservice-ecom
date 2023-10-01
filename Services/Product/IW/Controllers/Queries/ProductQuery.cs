using IW.Common;
using IW.Interfaces;
using IW.Models.DTOs.Product;
using IW.Models.DTOs.ProductDto;

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

        public async Task<IEnumerable<ProductDto>> GetProducts([Service] IProductService productService, int offset = ((int)PAGINATING.OffsetDefault), int amount = ((int)PAGINATING.AmountDefault))
        {
            var results = await productService.GetProducts(offset, amount);
            return results;
        }

        public async Task<IEnumerable<ProductDto>> GetProducts(GetProduct query, [Service] IProductService productService, int offset = ((int)PAGINATING.OffsetDefault), int amount = ((int)PAGINATING.AmountDefault))
        {
            var results = await productService.GetProducts(query, offset, amount);
            return results;
        }
    }
}
