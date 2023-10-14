namespace IW.Exceptions.CreateCartError
{
    public class CreateCartErrorFactory
    : IPayloadErrorFactory<ValidateCartException, ValidateCartError>
    {
        public ValidateCartError CreateErrorFrom(ValidateCartException ex)
        {
            return new ValidateCartError(ex.Errors);
        }
    }
}