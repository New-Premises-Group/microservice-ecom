namespace IW.Exceptions.CreateAddressError
{
    public class CreateAddressErrorFactory
    : IPayloadErrorFactory<ValidateAddressException, ValidateAddressError>
    {
        public ValidateAddressError CreateErrorFrom(ValidateAddressException ex)
        {
            return new ValidateAddressError(ex.Errors);
        }
    }
}
