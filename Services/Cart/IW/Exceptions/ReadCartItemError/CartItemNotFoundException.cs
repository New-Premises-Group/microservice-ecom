using IW.Common;

namespace IW.Exceptions.ReadCartItemError
{
    public class CartItemNotFoundException : Exception
    {
        public CartItemNotFoundException(int id)
        : base($"The Cart Item with id {id} is not found.")
        {
            Id = id;
        }

        public int Id { get; }
    }

    public class CartItemNotFoundError : GenericQueryError
    {
        public CartItemNotFoundError(string message) : base(message)
        {
            Message = message;
            Code = StatusCodes.Status404NotFound.ToString();
        }
    }
}