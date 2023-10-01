
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
}
