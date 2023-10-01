
namespace IW.Exceptions.ReadItemError
{
    public class ReadProductErrorFactory
    : IPayloadErrorFactory<ItemNotFoundException, ItemNotFoundError>
    {
        public ItemNotFoundError CreateErrorFrom(ItemNotFoundException ex)
        {
            return new ItemNotFoundError(ex.Message);
        }
    }
    
}
