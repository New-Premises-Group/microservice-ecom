using IW.Common;

namespace IW.Exceptions.ReadInventoryError
{
    public class InventoryNotFoundException:Exception
    {
        public InventoryNotFoundException(int id)
        : base($"The inventory with id {id} is not found.")
        {
            Id = id;
        }
        public int Id { get; }
    }

    public class InventoryNotFoundError : GenericQueryError
    {
        public InventoryNotFoundError(string message):base(message)
        {
            Message = message;
            Code = StatusCodes.Status404NotFound.ToString();
        }
    }
}
