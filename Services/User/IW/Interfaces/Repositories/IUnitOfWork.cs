using IW.Interfaces.Repositories;

namespace IW.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        // Add your Repository here
        IUserRepository Users { get; }
        IRoleRepository Roles { get; }
        IAddressRepository Addresses { get; }
        Task<int> CompleteAsync();
    }
}
