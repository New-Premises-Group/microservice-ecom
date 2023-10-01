
namespace IW.Exceptions.CreateOrderError
{
    public class CreateOrderErrorFactory :
        IPayloadErrorFactory<ValidateOrderException, ValidateOrderError>
    {
        public ValidateOrderError CreateErrorFrom(ValidateOrderException ex)
        {
            return new ValidateOrderError(ex.Errors);
        }
    }
}
