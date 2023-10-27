using IW.Common;

namespace IW.Exceptions.ReadPaymentError
{
    public class PaymentNotFoundException : Exception
    {
        public PaymentNotFoundException(int id)
        : base($"The Payment with id {id} is not found.")
        {
            Id = id;
        }

        public int Id { get; }
    }

    public class PaymentNotFoundError : GenericQueryError
    {
        public PaymentNotFoundError(string message) : base(message)
        {
            Message = message;
            Code = StatusCodes.Status404NotFound.ToString();
        }
    }
}