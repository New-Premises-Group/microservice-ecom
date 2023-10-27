using IW.Interfaces.Services;
using IW.Models.DTOs.Address;

namespace IW.Controllers.Queries
{
    [ExtendObjectType("Query")]
    public class AddressQuery
    {
        public async Task<IEnumerable<AddressDto>> GetAddresses([Service] IAddressService addressService)
        {
            var address = await addressService.GetAddresses();
            return address;
        }

        public async Task<AddressDto> GetAddress(int id, [Service] IAddressService addressService)
        {
            var address = await addressService.GetAddress(id);
            return address;
        }
    }
}
