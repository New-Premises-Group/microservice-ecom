using IW.Interfaces.Repositories;

namespace IW.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        // Add your Repository here
        IPaymentRepository Payments { get; }

        Task<int> CompleteAsync();
    }
}