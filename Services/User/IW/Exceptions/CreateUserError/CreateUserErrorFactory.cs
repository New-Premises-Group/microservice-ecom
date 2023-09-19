namespace IW.Exceptions.CreateUserError
{
    public class CreateUserErrorFactory
    : IPayloadErrorFactory<UserNameTakenException, UserNameTakenError>
    , IPayloadErrorFactory<UserEmailTakenException, UserEmailTakenError>
    , IPayloadErrorFactory<ValidateUserException, ValidateUserError>
    {
        public UserEmailTakenError CreateErrorFrom(UserEmailTakenException ex)
        {
            return new UserEmailTakenError(ex.Email);
        }

        public UserNameTakenError CreateErrorFrom(UserNameTakenException ex)
        {
            return new UserNameTakenError(ex.Name);
        }

        public ValidateUserError CreateErrorFrom(ValidateUserException ex)
        {
            return new ValidateUserError(ex.Errors);
        }
    }
}
