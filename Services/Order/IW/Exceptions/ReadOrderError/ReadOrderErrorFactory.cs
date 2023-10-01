
namespace IW.Exceptions.ReadOrderError
{
    public class ReadOrderErrorFactory : 
        IPayloadErrorFactory<OrderNotFoundException, OrderNotFoundError>
    {
        public OrderNotFoundError CreateErrorFrom(OrderNotFoundException ex)
        {
            return new OrderNotFoundError(ex.Message);
        }
    }
}
