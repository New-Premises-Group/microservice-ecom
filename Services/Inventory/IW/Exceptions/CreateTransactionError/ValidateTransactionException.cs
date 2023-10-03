using IW.Common;

namespace IW.Exceptions.CreateTransactionError
{
    public class ValidateTransactionException : Exception
    {
        public ValidateTransactionException(List<ValidateErrorDetail> errors)
        : base($"Input validate error please check the following field(s):\n {errors.ToString}")
        {
            Errors = errors;
        }
        public List<ValidateErrorDetail> Errors { get; }
    }
    public class ValidateTransactionError
    {
        public ValidateTransactionError(List<ValidateErrorDetail> errors)
        {
            Message = $"Input validate error please check the following field(s):\n {errors.ToString}";
        }
        public string Message { get; }
    }
}
