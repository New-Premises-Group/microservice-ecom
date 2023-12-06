using IW.Common;

namespace IW.Exceptions.CreateDiscountError
{
    public class ValidateDiscountException : Exception
    {
        public ValidateDiscountException(List<ValidateErrorDetail> errors)
        : base($"Input validate error please check the following field(s):\n {errors}")
        {
            Errors = errors;
        }
        public List<ValidateErrorDetail> Errors { get; }
    }
    public class ValidateDiscountError 
    {
        public ValidateDiscountError(List<ValidateErrorDetail> errors)
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
