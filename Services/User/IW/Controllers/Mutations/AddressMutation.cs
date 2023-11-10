using IW.Exceptions.CreateAddressError;
using IW.Interfaces.Services;
using IW.Models.DTOs.Address;

namespace IW.Controllers.Mutations
{
    [ExtendObjectType("Mutation")]
    public class AddressMutation
    {
        [Error(typeof(CreateAddressErrorFactory))]
        public async Task<AddressCreatedPayload> CreateAddress(CreateAddress address, [Service] IAddressService addressService)
        {
            var id=await addressService.CreateAddress(address);
            var payload = new AddressCreatedPayload()
            {
                Id=id,
                Message = "Address successfully created"
            };
            return payload;
        }

        [Error(typeof(CreateAddressErrorFactory))]
        public async Task<AddressCreatedPayload> UpdateAddress(int id, UpdateAddress input, [Service] IAddressService addressService)
        {
            await addressService.UpdateAddress(id, input);
            var payload = new AddressCreatedPayload()
            {
                Message = "Address successfully updated"
            };
            return payload;
        }

        public async Task<AddressDeletedPayload> DeleteAddress(int id, [Service] IAddressService addressService)
        {
            await addressService.DeleteAddress(id);
            var payload = new AddressDeletedPayload()
            {
                Message = "Address successfully deleted"
            };
            return payload;
        }
    }
}
