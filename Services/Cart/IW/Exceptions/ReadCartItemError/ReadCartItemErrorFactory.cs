namespace IW.Exceptions.ReadCartItemError
{
    public class ReadCartItemErrorFactory :
        IPayloadErrorFactory<CartItemNotFoundException, CartItemNotFoundError>
    {
        public CartItemNotFoundError CreateErrorFrom(CartItemNotFoundException ex)
        {
            return new CartItemNotFoundError(ex.Message);
        }
    }
}