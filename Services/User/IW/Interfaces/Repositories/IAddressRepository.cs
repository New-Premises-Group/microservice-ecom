using IW.Models.Entities;

namespace IW.Interfaces
{
    public interface IAddressRepository: IBaseRepository<Address>
    {
        public Task SetDefaultAddress(int id);
    }
}
