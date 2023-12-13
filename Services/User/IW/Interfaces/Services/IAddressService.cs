using IW.Models.DTOs.Address;

namespace IW.Interfaces.Services
{
    public interface IAddressService
    {
        Task<AddressDto> GetAddress(int id);
        Task<ICollection<AddressDto>> GetAddresses(int amount, int page);
        Task<ICollection<AddressDto>> GetAddresses(GetAddressQuery query, int amount, int page);
        Task<int> CreateAddress(CreateAddress input);
        Task UpdateAddress(int id, UpdateAddress input);
        public Task SetDefaultAddress(int id, string userId);
        Task DeleteAddress(int id);
    }
}
