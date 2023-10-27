namespace IW.Exceptions.CreatePaymentError
{
    public class CreatePaymentErrorFactory :
        IPayloadErrorFactory<ValidatePaymentException, ValidatePaymentError>
    {
        public ValidatePaymentError CreateErrorFrom(ValidatePaymentException ex)
        {
            return new ValidatePaymentError(ex.Errors);
        }
    }
}