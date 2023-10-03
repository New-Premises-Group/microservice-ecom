
namespace IW.Exceptions.CreateTransactionError
{
    public class CreateTransactionErrorFactory :
        IPayloadErrorFactory<ValidateTransactionException, ValidateTransactionError>
    {
        public ValidateTransactionError CreateErrorFrom(ValidateTransactionException ex)
        {
            return new ValidateTransactionError(ex.Errors);
        }
    }
}
