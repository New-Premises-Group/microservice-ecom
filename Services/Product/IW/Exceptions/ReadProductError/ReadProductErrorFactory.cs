
namespace IW.Exceptions.ReadProductError
{
    public class ReadProductErrorFactory
    : IPayloadErrorFactory<ProductNotFoundException, ProductNotFoundError>
    {
        public ProductNotFoundError CreateErrorFrom(ProductNotFoundException ex)
        {
            return new ProductNotFoundError(ex.Message);
        }
    }
    
}
