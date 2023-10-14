namespace IW.Exceptions.ReadCartError
{
    public class ReadCartErrorFactory
    : IPayloadErrorFactory<CartNotFoundException, CartNotFoundError>
    {
        public CartNotFoundError CreateErrorFrom(CartNotFoundException ex)
        {
            return new CartNotFoundError(ex.Message);
        }
    }
}