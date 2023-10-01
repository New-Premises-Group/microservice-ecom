using IW.Exceptions.ReadCategoryError;
using IW.Exceptions.ReadProductError;

namespace IW.Exceptions
{
    public class ErrorFilter : IErrorFilter
    {
        public IError OnError(IError error)
        {
            return error.Exception switch
            {
                CategoryNotFoundException => new ReadCategoryErrorFactory().CreateErrorFrom((CategoryNotFoundException)error.Exception).WithExtensions(),
                ProductNotFoundException => new ReadProductErrorFactory().CreateErrorFrom((ProductNotFoundException)error.Exception).WithExtensions(),
                _ => error,
            };
        }
    }
}
