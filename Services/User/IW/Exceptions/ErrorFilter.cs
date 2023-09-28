using IW.Exceptions.ReadUserError;

namespace IW.Exceptions
{
    public class ErrorFilter : IErrorFilter
    {
        public IError OnError(IError error)
        {
            switch (error.Exception)
            {
                case UserNotFoundException:
                    return new ReadUserErrorFactory().CreateErrorFrom((UserNotFoundException)error.Exception).WithExtensions();
                default:
                    return error;
            }
        }
    }
}
