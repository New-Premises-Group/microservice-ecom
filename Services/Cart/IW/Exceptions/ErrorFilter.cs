using IW.Exceptions.ReadCartError;
using IW.Exceptions.ReadCartItemError;

namespace IW.Exceptions
{
    public class ErrorFilter : IErrorFilter
    {
        public IError OnError(IError error)
        {
            return error.Exception switch
            {
                CartItemNotFoundException => new ReadCartItemErrorFactory().CreateErrorFrom((CartItemNotFoundException)error.Exception).WithExtensions(),
                CartNotFoundException => new ReadCartErrorFactory().CreateErrorFrom((CartNotFoundException)error.Exception).WithExtensions(),
                _ => error,
            };
        }
    }
}