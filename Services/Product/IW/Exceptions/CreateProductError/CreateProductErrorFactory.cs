
namespace IW.Exceptions.CreateProductError
{
    public class CreateProductErrorFactory
    :   IPayloadErrorFactory<ValidateProductException, ValidateProductError>,
        IPayloadErrorFactory<ProductNameTakenException, ProductNameTakenError>
    {
        public ProductNameTakenError CreateErrorFrom(ProductNameTakenException ex)
        {
            return new ProductNameTakenError(ex.Name);
        }

        public ValidateProductError CreateErrorFrom(ValidateProductException ex)
        {
            return new ValidateProductError(ex.Errors);
        }
    }
}
