
namespace IW.Exceptions.ReadTransactionError
{
    public class ReadTransactionErrorFactory : 
        IPayloadErrorFactory<TransactionNotFoundException, TransactionNotFoundError>
    {
        public TransactionNotFoundError CreateErrorFrom(TransactionNotFoundException ex)
        {
            return new TransactionNotFoundError(ex.Message);
        }
    }
}
