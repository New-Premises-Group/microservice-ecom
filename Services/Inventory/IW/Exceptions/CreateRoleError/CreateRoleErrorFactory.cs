using IW.Exceptions.CreateUserError;

namespace IW.Exceptions.CreateRoleError
{
    public class CreateRoleErrorFactory :
        IPayloadErrorFactory<ValidateRoleException, ValidateRoleError>
    {
        public ValidateRoleError CreateErrorFrom(ValidateRoleException ex)
        {
            return new ValidateRoleError(ex.Errors);
        }
    }
}
