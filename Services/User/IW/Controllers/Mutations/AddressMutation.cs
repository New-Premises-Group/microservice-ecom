using HotChocolate.Authorization;
using IW.Common;
using IW.Exceptions.CreateAddressError;
using IW.Interfaces.Services;
using IW.Models.DTOs.Address;
using IW.Models.Entities;

namespace IW.Controllers.Mutations
{
    [ExtendObjectType("Mutation")]
    public class AddressMutation
    {
        [Error(typeof(CreateAddressErrorFactory))]
        public async Task<AddressCreatedPayload> CreateAddress(
            CreateAddress address,
            [Service] IAddressService addressService)
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
        public async Task<AddressUpdatedPayload> UpdateAddress(
            int id, 
            UpdateAddress input, 
            [Service] IAddressService addressService)
        {
            await addressService.UpdateAddress(id, input);
            var payload = new AddressUpdatedPayload()
            {
                Message = "Address successfully updated"
            };
            return payload;
        }

        [Error(typeof(CreateAddressErrorFactory))]
        public async Task<AddressUpdatedPayload> UpdateDefaultAddress(
            int id, 
            string userId,
            [Service] IAddressService addressService)
        {
            await addressService.SetDefaultAddress(id, userId);
            var payload = new AddressUpdatedPayload()
            {
                Message = "Default address successfully updated"
            };
            return payload;
        }

        public async Task<AddressDeletedPayload> DeleteAddress(
            int id, 
            [Service] IAddressService addressService)
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
