namespace IW.Exceptions.ReadInventoryError
{
    public class ReadInventoryErrorFactory
    : IPayloadErrorFactory<InventoryNotFoundException, InventoryNotFoundError>
    {
        public InventoryNotFoundError CreateErrorFrom(InventoryNotFoundException ex)
        {
            return new InventoryNotFoundError(ex.Message);
        }
    }
    
}
