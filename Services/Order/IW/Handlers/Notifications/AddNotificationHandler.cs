using IW.Common;
using IW.Exceptions.ReadOrderError;
using IW.Interfaces;
using IW.Interfaces.Services;
using IW.Models;
using IW.Models.DTOs;
using IW.Models.DTOs.Item;
using IW.Models.DTOs.OrderDtos;
using IW.Notifications;
using MapsterMapper;
using System.Collections.Generic;

namespace IW.Handlers.Notifications
{
    public class AddNotificationHandler : AbstractCommandHandler
    {
        private readonly IRabbitMqProducer _producer;
        private readonly IMailService _mailService;
        public AddNotificationHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IMailService mailService,
            IRabbitMqProducer producer,
            IMediator mediator) : base(unitOfWork, mapper, mediator)
        {
            _mailService = mailService;
            _producer = producer;
        }

        private async Task<int> Handle(CreateNotification request)
        {
            var sendToQueueTask = Task.Run(() =>
            {
                _producer.Send(nameof(QUEUE_NAME.Order_Placed), request.Message);
                
            });
            var sendEmailTask = _mailService.Send(
                    request.Email,
                    request.UserName,
                    _mapper.Map<ICollection<ItemDto>>(request.Items),
                    request.Total);
            await Task.WhenAll(sendToQueueTask, sendEmailTask);

            return request.Message.Id;
        }

        public override async Task<int> Handle<TRequest>(TRequest request)
        {
            return request switch
            {
                CreateNotification notifiRequest => await Handle(notifiRequest),
                _ => throw new ArgumentException(),
            };
        }

        public override async Task Undo()
        {
            
        }
    }
}
