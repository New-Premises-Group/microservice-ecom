using IW.Common;

namespace IW.Exceptions.CreatePaymentError
{
    public class ValidatePaymentException : Exception
    {
        public ValidatePaymentException(List<ValidateErrorDetail> errors)
        : base($"Input validate error please check the following field(s):\n {errors.ToString}")
        {
            Errors = errors;
        }

        public List<ValidateErrorDetail> Errors { get; }
    }

    public class ValidatePaymentError
    {
        public ValidatePaymentError(List<ValidateErrorDetail> errors)
        {
            Message = $"Input validate error please check the following field(s):\n {errors.ToString}";
        }

        public string Message { get; }
    }
}