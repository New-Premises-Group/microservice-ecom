
namespace IW.Exceptions.ReadCategoryError
{
    public class ReadCategoryErrorFactory : 
        IPayloadErrorFactory<CategoryNotFoundException, CategoryNotFoundError>
    {
        public CategoryNotFoundError CreateErrorFrom(CategoryNotFoundException ex)
        {
            return new CategoryNotFoundError(ex.Message);
        }
    }
}
