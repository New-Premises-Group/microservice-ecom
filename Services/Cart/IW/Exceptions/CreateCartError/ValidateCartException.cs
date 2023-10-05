using IW.Common;

namespace IW.Exceptions.CreateCartError
{
    public class ValidateCartException : Exception
    {
        public ValidateCartException(List<ValidateErrorDetail> errors)
        : base($"Input validate error please check the following field(s):\n {errors.ToString}")
        {
            Errors = errors;
        }

        public List<ValidateErrorDetail> Errors { get; }
    }

    public class ValidateCartError
    {
        public ValidateCartError(List<ValidateErrorDetail> errors)
        {
            Message = $"Input validate error please check the following field(s):\n {errors.ToString}";
        }

        public string Message { get; }
    }
}