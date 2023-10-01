using IW.Common;

namespace IW.Exceptions.CreateCategoryError
{
    public class ValidateCategoryException : Exception
    {
        public ValidateCategoryException(List<ValidateErrorDetail> errors)
        : base($"Input validate error please check the following field(s):\n {errors.ToString}")
        {
            Errors = errors;
        }
        public List<ValidateErrorDetail> Errors { get; }
    }
    public class ValidateCategoryError
    {
        public ValidateCategoryError(List<ValidateErrorDetail> errors)
        {
            Message = $"Input validate error please check the following field(s):\n {errors.ToString}";
        }
        public string Message { get; }
    }
}
