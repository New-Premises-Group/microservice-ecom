using IW.Interfaces;
using HotChocolate.Authorization;
using IW.Common;
using IW.Exceptions.CreateProductError;
using IW.Models.DTOs.Product;
using IW.Models.DTOs;

namespace IW.Controllers.Mutations
{
    [ExtendObjectType("Mutation")]
    [Authorize(Roles = new[] { nameof(ROLE.Admin) })]
    public class ProductMutation
    {
        [Error(typeof(CreateProductErrorFactory))]
        public async Task<ProductCreatedPayload> CreateProduct(CreateProduct input, [Service] IProductService productService)
        {
            await productService.CreateProduct(input);
            var payload = new ProductCreatedPayload()
            {
                Message = "Product successfully created"
            };
            return payload;
        }

        [Error(typeof(CreateProductErrorFactory))]
        public async Task<ProductUpdatedPayload> UpdateProduct(int id, UpdateProduct input, [Service] IProductService productService)
        {
            await productService.UpdateProduct(id, input);
            var payload = new ProductUpdatedPayload()
            {
                Message = "Product successfully updated",
            };
            return payload;
        }

        public async Task<ProductDeletedPayload> DeleteProduct(int id, [Service] IProductService productService)
        {
            await productService.DeleteProduct(id);
            var payload = new ProductDeletedPayload()
            {
                Message = "Product successfully deleted"
            };
            return payload;
        }
    }
}
