using IW.Exceptions.ReadTransactionError;
using IW.Exceptions.ReadInventoryError;

namespace IW.Exceptions
{
    public class ErrorFilter : IErrorFilter
    {
        public IError OnError(IError error)
        {
            return error.Exception switch
            {
                TransactionNotFoundException => new ReadTransactionErrorFactory().CreateErrorFrom((TransactionNotFoundException)error.Exception).WithExtensions(),
                InventoryNotFoundException => new ReadInventoryErrorFactory().CreateErrorFrom((InventoryNotFoundException)error.Exception).WithExtensions(),
                _ => error,
            };
        }
    }
}
