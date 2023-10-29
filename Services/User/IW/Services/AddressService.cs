﻿using IW.Exceptions.ReadRoleError;
using IW.Interfaces;
using IW.Interfaces.Services;
using IW.Models.DTOs.Address;
using IW.Models.DTOs.Role;
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

        public async Task CreateAddress(CreateAddress input)
        {
            Address newAddress = input.Adapt<Address>();

            AddressValidator validator = new();
            validator.ValidateAndThrowException(newAddress);

            _unitOfWork.Addresses.Add(input.Adapt<Address>());
            await _unitOfWork.CompleteAsync();
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

        public async Task<ICollection<AddressDto>> GetAddresses()
        {
            ICollection<Address> addresses=await _unitOfWork.Addresses.GetAll(0, 0);

            return addresses.Adapt<ICollection<AddressDto>>();
        }

        public async Task UpdateAddress(int id, UpdateAddress model)
        {
            Address address =await AddressExist(id);
            address.Detail= model.Detail;
            address.Ward= model.Ward;
            address.City= model.City;
            address.District= model.District;

            AddressValidator validator = new();
            validator.ValidateAndThrowException(address);

            _unitOfWork.Addresses.Update(address);
            await _unitOfWork.CompleteAsync();
        }

        private async Task<Address?> AddressExist(int id)
        {
            return await _unitOfWork.Addresses.GetById(id);

        }
    }
}