using IW.Common;

namespace IW.Exceptions.CreateRoleError
{
    public class ValidateRoleException : Exception
    {
        public ValidateRoleException(List<ValidateErrorDetail> errors)
        : base($"Input validate error please check the following field(s):\n {errors.ToString}")
        {
            Errors = errors;
        }
        public List<ValidateErrorDetail> Errors { get; }
    }
    public class ValidateRoleError
    {
        public ValidateRoleError(List<ValidateErrorDetail> errors)
        {
            Message = $"Input validate error please check the following field(s):\n {errors.ToString}";
        }
        public string Message { get; }
    }
}
