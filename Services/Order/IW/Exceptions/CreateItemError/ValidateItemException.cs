using IW.Common;

namespace IW.Exceptions.CreateItemError
{
    public class ValidateItemException : Exception
    {
        public ValidateItemException( List<ValidateErrorDetail> errors)
        : base($"Input validate error please check the following field(s):\n {errors.ToString}")
        {
            Errors = errors;
        }
        public List<ValidateErrorDetail> Errors { get; }
    }
    public class ValidateItemError
    {
        public ValidateItemError(List<ValidateErrorDetail> errors)
        {
            Message = $"Input validate error please check the following field(s):\n {errors.ToString}";
        }
        public string Message { get; }
    }
}
