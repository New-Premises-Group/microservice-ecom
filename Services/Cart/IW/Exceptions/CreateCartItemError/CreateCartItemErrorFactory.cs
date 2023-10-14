namespace IW.Exceptions.CreateCartItemError
{
    public class CreateCartItemErrorFactory :
        IPayloadErrorFactory<ValidateCartItemException, ValidateCartItemError>
    {
        public ValidateCartItemError CreateErrorFrom(ValidateCartItemException ex)
        {
            return new ValidateCartItemError(ex.Errors);
        }
    }
}