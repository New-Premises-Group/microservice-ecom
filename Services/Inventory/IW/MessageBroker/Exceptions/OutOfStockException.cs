namespace IW.MessageBroker.Exceptions
{
    public class OutOfStockException:Exception
    {
        public OutOfStockException(int productId)
            :base($"Product with id {productId} is out of stock")
        {
        }

        public int ProductId { get; set; }
    }
}
