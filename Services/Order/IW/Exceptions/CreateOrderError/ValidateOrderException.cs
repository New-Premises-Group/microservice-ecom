using IW.Common;

namespace IW.Exceptions.CreateOrderError
{
    public class ValidateOrderException : Exception
    {
        public ValidateOrderException(List<ValidateErrorDetail> errors)
        : base($"Input validate error please check the following field(s):\n {errors}")
        {
            Errors = errors;
        }
        public List<ValidateErrorDetail> Errors { get; }
    }
    public class ValidateOrderError 
    {
        public ValidateOrderError(List<ValidateErrorDetail> errors)
        {
            Message = $"Input validate error please check the following field(s): ";
            foreach (ValidateErrorDetail detail in errors)
            {
                Message += $"{detail} ";
            }
        }
        public string Message { get; }
    }
}
