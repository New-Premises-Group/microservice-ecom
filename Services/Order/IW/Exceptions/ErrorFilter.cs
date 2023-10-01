using IW.Exceptions.ReadOrderError;
using IW.Exceptions.ReadItemError;

namespace IW.Exceptions
{
    public class ErrorFilter : IErrorFilter
    {
        public IError OnError(IError error)
        {
            return error.Exception switch
            {
                OrderNotFoundException => new ReadOrderErrorFactory().CreateErrorFrom((OrderNotFoundException)error.Exception).WithExtensions(),
                ItemNotFoundException => new ReadProductErrorFactory().CreateErrorFrom((ItemNotFoundException)error.Exception).WithExtensions(),
                _ => error,
            };
        }
    }
}
