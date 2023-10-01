using IW.Common;

namespace IW.Exceptions.ReadCategoryError
{
    public class CategoryNotFoundException : Exception
    {
        public CategoryNotFoundException(int id)
        : base($"The category with id {id} is not found.")
        {
            Id = id;
        }
        public CategoryNotFoundException(string name)
        : base($"The category with name {name} is not found.")
        {
            Name = name;
        }
        public int Id { get; }
        public string? Name { get; }
    }

    public class CategoryNotFoundError : GenericQueryError
    {
        public CategoryNotFoundError(string message) : base(message)
        {
            Message = message;
            Code = StatusCodes.Status404NotFound.ToString();
        }
    }
}
