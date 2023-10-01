using IW.Common;

namespace IW.Exceptions.CreateProductError
{
    public class ValidateProductException : Exception
    {
        public ValidateProductException( List<ValidateErrorDetail> errors)
        : base($"Input validate error please check the following field(s):\n {errors.ToString}")
        {
            Errors = errors;
        }
        public List<ValidateErrorDetail> Errors { get; }
    }
    public class ValidateProductError
    {
        public ValidateProductError(List<ValidateErrorDetail> errors)
        {
            Message = $"Input validate error please check the following field(s):\n {errors.ToString}";
        }
        public string Message { get; }
    }
}
