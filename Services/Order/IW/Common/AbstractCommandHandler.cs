using IW.Interfaces;
using IW.Interfaces.Commands;
using IW.Interfaces.Services;
using IW.Models;
using MapsterMapper;

namespace IW.Common
{
    public abstract class AbstractCommandHandler: ICommandHandler
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;
        protected readonly IMediator _mediator;

        public AbstractCommandHandler(
            IUnitOfWork unitOfWork, 
            IMapper mapper,
            IMediator mediator) 
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _mediator = mediator;
        }

        public abstract Task<int> Handle <TRequest>(TRequest request);

        public abstract Task Undo();
    }
}
