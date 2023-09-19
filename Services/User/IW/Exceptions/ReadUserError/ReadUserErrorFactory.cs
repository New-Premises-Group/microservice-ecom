using IW.Exceptions.CreateUserError;

namespace IW.Exceptions.ReadUserError
{
    public class ReadUserErrorFactory
    : IPayloadErrorFactory<UserNotFoundException, UserNotFoundError>
    {
        public UserNotFoundError CreateErrorFrom(UserNotFoundException ex)
        {
            return new UserNotFoundError(ex.Message);
        }
    }
    public class ErrorFilter : IErrorFilter
    {
        public IError OnError(IError error)
        {
            switch (error.Exception)
            {
                case UserNotFoundException:
                    return new ReadUserErrorFactory().CreateErrorFrom((UserNotFoundException)error.Exception).WithCode();
                default:
                    return error;
            }
        }
    }
}
