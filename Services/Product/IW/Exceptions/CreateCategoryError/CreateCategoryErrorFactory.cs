
namespace IW.Exceptions.CreateCategoryError
{
    public class CreateCategoryErrorFactory :
        IPayloadErrorFactory<ValidateCategoryException, ValidateCategoryError>
    {
        public ValidateCategoryError CreateErrorFrom(ValidateCategoryException ex)
        {
            return new ValidateCategoryError(ex.Errors);
        }
    }
    public class DeleteCategoryErrorFactory :
      IPayloadErrorFactory<ValidateCategoryException, ValidateCategoryError>
    {
        public ValidateCategoryError CreateErrorFrom(ValidateCategoryException ex)
        {
            return new ValidateCategoryError(ex.Errors);
        }
    }
}
