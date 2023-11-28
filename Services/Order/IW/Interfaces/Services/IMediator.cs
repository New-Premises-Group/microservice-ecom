using IW.Interfaces.Commands;

namespace IW.Interfaces.Services
{
    public interface IMediator
    {
        Task<int> Send<TRequest>(TRequest request);
    }
}
