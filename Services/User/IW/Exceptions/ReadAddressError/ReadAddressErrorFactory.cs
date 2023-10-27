

namespace IW.Exceptions.ReadAddressError
{
    public class ReadAddressErrorFactory : 
        IPayloadErrorFactory<AddressNotFoundException, AddressNotFoundError>
    {
        public AddressNotFoundError CreateErrorFrom(AddressNotFoundException ex)
        {
            return new AddressNotFoundError(ex.Message);
        }
    }
}
