namespace IW.Exceptions.CreateProductError
{
    public class ProductNameTakenException : Exception
    {
        public ProductNameTakenException(string name)
        : base($"The product {name} is already taken.")
        {
            Name = name;
        }
        public string Name { get; }
    }
    public class ProductNameTakenError
    {
        public ProductNameTakenError(string name)
        {
            Message = $"The name {name} is already taken.";
        }
        public string Message { get; }
    }
}
