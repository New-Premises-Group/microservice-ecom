
namespace IW.Exceptions.CreateDiscountError
{
    public class CreateDiscountErrorFactory :
        IPayloadErrorFactory<ValidateDiscountException, ValidateDiscountError>
    {
        public ValidateDiscountError CreateErrorFrom(ValidateDiscountException ex)
        {
            return new ValidateDiscountError(ex.Errors);
        }
    }
}
