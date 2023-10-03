using IW.Common;

namespace IW.Exceptions.CreateInventoryError
{
    public class ValidateInventoryException : Exception
    {
        public ValidateInventoryException( List<ValidateErrorDetail> errors)
        : base($"Input validate error please check the following field(s):\n {errors.ToString}")
        {
            Errors = errors;
        }
        public List<ValidateErrorDetail> Errors { get; }
    }
    public class ValidateInventoryError
    {
        public ValidateInventoryError(List<ValidateErrorDetail> errors)
        {
            Message = $"Input validate error please check the following field(s):\n {errors.ToString}";
        }
        public string Message { get; }
    }
}
