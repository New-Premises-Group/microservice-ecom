using IW.Common;

namespace IW.Exceptions.ReadTransactionError
{
    public class TransactionNotFoundException : Exception
    {
        public TransactionNotFoundException(int id)
        : base($"The transaction with id {id} is not found.")
        {
            Id = id;
        }
        public int Id { get; }
    }

    public class TransactionNotFoundError : GenericQueryError
    {
        public TransactionNotFoundError(string message) : base(message)
        {
            Message = message;
            Code = StatusCodes.Status404NotFound.ToString();
        }
    }
}
