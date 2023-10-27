using IW.Common;

namespace IW.Exceptions.CreateAddressError
{
    public class ValidateAddressException : Exception
    {
        public ValidateAddressException( List<ValidateErrorDetail> errors)
        : base($"Input validate error please check the following field(s):\n {errors.ToString}")
        {
            Errors = errors;
        }
        public List<ValidateErrorDetail> Errors { get; }
    }
    public class ValidateAddressError
    {
        public ValidateAddressError(List<ValidateErrorDetail> errors)
        {
            Message = $"Input validate error please check the following field(s):\n {errors.ToString}";
        }
        public string Message { get; }
    }
}
