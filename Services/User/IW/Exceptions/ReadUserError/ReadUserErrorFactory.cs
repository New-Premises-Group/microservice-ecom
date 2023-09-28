
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
    
}
