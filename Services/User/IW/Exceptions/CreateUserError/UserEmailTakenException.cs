namespace IW.Exceptions.CreateUserError
{
    public class UserEmailTakenException : Exception
    {
        public UserEmailTakenException(string email)
        {
            Email = email;
        }
        public string Email { get; }
    }

    public class UserEmailTakenError
    {
        public UserEmailTakenError(string email)
        {
            Message = $"The email {email} is already taken.";
        }
        public string Message { get; }
    }
}
