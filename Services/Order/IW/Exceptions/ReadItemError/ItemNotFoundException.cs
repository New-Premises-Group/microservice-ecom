using IW.Common;

namespace IW.Exceptions.ReadItemError
{
    public class ItemNotFoundException:Exception
    {
        public ItemNotFoundException(int id)
        : base($"The item with id {id} is not found.")
        {
            Id = id;
        }
        public int Id { get; }
    }

    public class ItemNotFoundError : GenericQueryError
    {
        public ItemNotFoundError(string message):base(message)
        {
            Message = message;
            Code = StatusCodes.Status404NotFound.ToString();
        }
    }
}
