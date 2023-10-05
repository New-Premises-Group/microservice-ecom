using IW.Common;

namespace IW.Exceptions.CreateCartItemError
{
    public class ValidateCartItemException : Exception
    {
        public ValidateCartItemException(List<ValidateErrorDetail> errors)
        : base($"Input validate error please check the following field(s):\n {errors.ToString}")
        {
            Errors = errors;
        }

        public List<ValidateErrorDetail> Errors { get; }
    }

    public class ValidateCartItemError
    {
        public ValidateCartItemError(List<ValidateErrorDetail> errors)
        {
            Message = $"Input validate error please check the following field(s):\n {errors.ToString}";
        }

        public string Message { get; }
    }
}