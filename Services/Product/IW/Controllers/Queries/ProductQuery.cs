using IW.Common;
using IW.Interfaces;
using IW.Models.DTOs.Product;
using IW.Models.DTOs.ProductDto;

namespace IW.Controllers.Queries
{
    [ExtendObjectType("Query")]
    public class ProductQuery
    {
        public async Task<ProductDto> GetUser(int id, [Service] IProductService productService)
        {
            var result = await productService.GetProduct(id);
            return result;
        }

        public async Task<IEnumerable<ProductDto>> GetUsers([Service] IProductService productService)
        {
            var results = await productService.GetProducts(((int)PAGINATING.OffsetDefault), ((int)PAGINATING.AmountDefault));
            return results;
        }

        public async Task<IEnumerable<ProductDto>> GetUsers(GetProduct query, [Service] IProductService productService)
        {
            var results = await productService.GetProducts(query, ((int)PAGINATING.OffsetDefault), ((int)PAGINATING.AmountDefault));
            return results;
        }
    }
}
