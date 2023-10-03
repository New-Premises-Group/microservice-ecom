
namespace IW.Exceptions.CreateInventoryError
{
    public class CreateInventoryErrorFactory
    :   IPayloadErrorFactory<ValidateInventoryException, ValidateInventoryError>
    {
        public ValidateInventoryError CreateErrorFrom(ValidateInventoryException ex)
        {
            return new ValidateInventoryError(ex.Errors);
        }
    }
}
