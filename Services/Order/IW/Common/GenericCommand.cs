using IW.Interfaces.Commands;
using IW.Models.DTOs.OrderDtos;

namespace IW.Common
{
    public abstract class GenericCommand: ICommand<IRequest>
    {
        private readonly AbstractCommandHandler _handler;
        private readonly IRequest _request;

        public GenericCommand(
            AbstractCommandHandler handler,
            IRequest request)
        {
            _handler = handler;
            _request = request;
        }

        public async Task<int> Execute()
        {
            try
            {
                return await _handler.Handle(_request);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }

        public async Task Undo()
        {
            await _handler.Undo();
        }
    }
}
