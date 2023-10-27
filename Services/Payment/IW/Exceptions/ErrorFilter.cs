using IW.Exceptions.ReadPaymentError;

namespace IW.Exceptions
{
    public class ErrorFilter : IErrorFilter
    {
        public IError OnError(IError error)
        {
            return error.Exception switch
            {
                PaymentNotFoundException => new ReadPaymentErrorFactory().CreateErrorFrom((PaymentNotFoundException)error.Exception).WithExtensions(),
                _ => error,
            };
        }
    }
}