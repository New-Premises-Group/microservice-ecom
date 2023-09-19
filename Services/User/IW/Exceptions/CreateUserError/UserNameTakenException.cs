namespace IW.Exceptions.CreateUserError
{
    public class UserNameTakenException : Exception
    {
        public UserNameTakenException(string name)
        : base($"The username {name} is already taken.")
        {
            Name = name;
        }
        public string Name { get; }
    }
    public class UserNameTakenError
    {
        public UserNameTakenError(string name)
        {
            Message = $"The name {name} is already taken.";
        }
        public string Message { get; }
    }
}
