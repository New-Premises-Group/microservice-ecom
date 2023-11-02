using IW.Exceptions.ReadAddressError;
using IW.Exceptions.ReadRoleError;
using IW.Exceptions.ReadUserError;

namespace IW.Exceptions
{
    public class ErrorFilter : IErrorFilter
    {
        public IError OnError(IError error)
        {
            return error.Exception switch
            {
                UserNotFoundException => new ReadUserErrorFactory().CreateErrorFrom((UserNotFoundException)error.Exception).WithExtensions(),
                RoleNotFoundException => new ReadRoleErrorFactory().CreateErrorFrom((RoleNotFoundException)error.Exception).WithExtensions(),
                AddressNotFoundException => new ReadAddressErrorFactory().CreateErrorFrom((AddressNotFoundException)error.Exception).WithExtensions(),
                _ => error,
            };
        }
    }
}
