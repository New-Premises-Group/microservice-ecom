using IW.Repositories;

namespace IW.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        // Add your Repository here
        IUserRepository Users { get; }
        Task<int> CompleteAsync();
    }
}
