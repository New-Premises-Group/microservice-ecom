using IW.Common;

namespace IW.Exceptions.ReadCartError
{
    public class CartNotFoundException : Exception
    {
        public CartNotFoundException(int id)
        : base($"The Cart with id {id} is not found.")
        {
            Id = id;
        }

        public int Id { get; }
    }

    public class CartNotFoundError : GenericQueryError
    {
        public CartNotFoundError(string message) : base(message)
        {
            Message = message;
            Code = StatusCodes.Status404NotFound.ToString();
        }
    }
}