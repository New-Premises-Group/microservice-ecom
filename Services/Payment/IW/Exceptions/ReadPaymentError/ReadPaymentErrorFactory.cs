namespace IW.Exceptions.ReadPaymentError
{
    public class ReadPaymentErrorFactory :
        IPayloadErrorFactory<PaymentNotFoundException, PaymentNotFoundError>
    {
        public PaymentNotFoundError CreateErrorFrom(PaymentNotFoundException ex)
        {
            return new PaymentNotFoundError(ex.Message);
        }
    }
}