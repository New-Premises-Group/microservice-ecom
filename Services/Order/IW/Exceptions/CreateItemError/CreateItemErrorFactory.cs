
namespace IW.Exceptions.CreateItemError
{
    public class CreateItemErrorFactory
    :   IPayloadErrorFactory<ValidateItemException, ValidateItemError>
    {
        public ValidateItemError CreateErrorFrom(ValidateItemException ex)
        {
            return new ValidateItemError(ex.Errors);
        }
    }
}
