using IW.Exceptions.ReadRoleError;
using IW.Interfaces;
using IW.Interfaces.Services;
using IW.Models.DTOs.Address;
using IW.Models.Entities;
using Mapster;

namespace IW.Services
{
    public class AddressService : IAddressService
    {
        public readonly IUnitOfWork _unitOfWork;
        public AddressService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> CreateAddress(CreateAddress input)
        {
            Address newAddress = input.Adapt<Address>();

            AddressValidator validator = new();
            validator.ValidateAndThrowException(newAddress);

            _unitOfWork.Addresses.Add(newAddress);
            await _unitOfWork.CompleteAsync();
            return newAddress.Id;
        }

        public async Task DeleteAddress(int id)
        {
            var address = await AddressExist(id);
            if (Equals(address, null)) throw new RoleNotFoundException(id);

            _unitOfWork.Addresses.Remove(address);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<AddressDto?> GetAddress(int id)
        {
            Address? address=await AddressExist(id);
            return address.Adapt<AddressDto?>();
        }

        public async Task<ICollection<AddressDto>> GetAddresses(int amount,int page)
        {
            ICollection<Address> addresses=await _unitOfWork.Addresses.GetAll(amount, page);

            return addresses.Adapt<ICollection<AddressDto>>();
        }

        public async Task<ICollection<AddressDto>> GetAddresses(GetAddressQuery query, int amount,int page)
        {
            ICollection<Address> addresses=await _unitOfWork.Addresses.FindByConditionToList(
                addr=>
                addr.Name==query.Name ||
                addr.Phone==query.Phone ||
                addr.UserId==query.UserId
                ,amount, page);

            return addresses.Adapt<ICollection<AddressDto>>();
        }

        public async Task UpdateAddress(int id, UpdateAddress model)
        {
            Address address =await AddressExist(id);
            address.Phone= model.Phone;
            address.Name= model.Name;
            address.Detail= model.Detail;
            address.Ward= model.Ward;
            address.City= model.City;
            address.District= model.District;

            AddressValidator validator = new();
            validator.ValidateAndThrowException(address);

            _unitOfWork.Addresses.Update(address);
            await _unitOfWork.CompleteAsync();
        }

        public async Task SetDefaultAddress(int id)
        {
            await _unitOfWork.Addresses.SetDefaultAddress(id);
        }

        private async Task<Address?> AddressExist(int id)
        {
            return await _unitOfWork.Addresses.GetById(id);

        }
    }
}
