using IW.Models.DTOs.OrderDtos;

namespace IW.Interfaces.Commands
{
    public interface ICommandHandler
    {
        Task<int> Handle<TRequest>(TRequest request);
        Task Undo();
    }
}
