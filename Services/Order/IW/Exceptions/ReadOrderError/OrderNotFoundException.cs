using IW.Common;

namespace IW.Exceptions.ReadOrderError
{
    public class OrderNotFoundException : Exception
    {
        public OrderNotFoundException(int id)
        : base($"The order with id {id} is not found.")
        {
            Id = id;
        }
        public OrderNotFoundException(Guid userId)
        : base($"The order with name {userId} is not found.")
        {
            userId = userId;
        }
        public int Id { get; }
        public Guid? userId { get; }
    }

    public class OrderNotFoundError : GenericQueryError
    {
        public OrderNotFoundError(string message) : base(message)
        {
            Message = message;
            Code = StatusCodes.Status404NotFound.ToString();
        }
    }
}
