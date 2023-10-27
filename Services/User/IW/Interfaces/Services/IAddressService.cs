using IW.Models.DTOs.Address;

namespace IW.Interfaces.Services
{
    public interface IAddressService
    {
        Task<AddressDto> GetAddress(int id);
        Task<ICollection<AddressDto>> GetAddresses();
        Task CreateAddress(CreateAddress input);
        Task UpdateAddress(int id, UpdateAddress input);
        Task DeleteAddress(int id);
    }
}
