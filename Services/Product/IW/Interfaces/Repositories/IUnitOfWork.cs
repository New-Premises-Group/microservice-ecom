using IW.Interfaces.Repositories;

namespace IW.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        // Add your Repository here
        IProductRepository Products { get; }
        ICategoryRepository Categories { get; }
        IReviewRepository Reviews { get; }
        Task<int> CompleteAsync();
    }
}
