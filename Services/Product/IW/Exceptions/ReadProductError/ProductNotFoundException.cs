using IW.Common;

namespace IW.Exceptions.ReadProductError
{
    public class ProductNotFoundException:Exception
    {
        public ProductNotFoundException(int id)
        : base($"The product with id {id} is not found.")
        {
            Id = id;
        }
        public ProductNotFoundException(string name)
        : base($"The product with name {name} is not found.")
        {
            Name = name;
        }
        public int Id { get; }
        public string? Name { get;  }
    }

    public class ProductNotFoundError : GenericQueryError
    {
        public ProductNotFoundError(string message):base(message)
        {
            Message = message;
            Code = StatusCodes.Status404NotFound.ToString();
        }
    }
}
