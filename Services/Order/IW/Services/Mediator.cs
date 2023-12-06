using IW.Commands.Discounts;
using IW.Commands.Items;
using IW.Commands.Notifications;
using IW.Commands.Orders;
using IW.Common;
using IW.Handlers.Discounts;
using IW.Handlers.Items;
using IW.Handlers.Notifications;
using IW.Handlers.Orders;
using IW.Interfaces.Services;
using IW.Models.DTOs;
using IW.Models.DTOs.DiscountDtos;
using IW.Models.DTOs.Item;
using IW.Models.DTOs.ItemDtos;
using IW.Models.DTOs.OrderDtos;
using Mapster;

namespace IW.Services
{
    public class Mediator : IMediator
    {
        //private IEnumerable<object> _handlers;
        private readonly IServiceProvider _serviceProvider;
        private readonly Stack<GenericCommand> _prevCommands;

        public Mediator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _prevCommands = new Stack<GenericCommand>();
        }

        public void Register()
        {

        }

        #region Order
        public async Task<int> Send(CreateGuestOrder request)
        {
            var handler = _serviceProvider.GetService<CreateGuestOrderHandler>();
            var command = new CreateGuestOrderCommand(handler, request);
            _prevCommands.Push(command);
            return await command.Execute();
        }

        public async Task<int> Send(CreateOrder request)
        {
            var handler = _serviceProvider.GetService<CreateOrderHandler>();
            var command = new CreateOrderCommand(handler, request);
            _prevCommands.Push(command);
            return await command.Execute();
        }

        public async Task<int> Send(UpdateOrder request)
        {
            var handler = _serviceProvider.GetService<UpdateOrderHandler>();
            var command = new UpdateOrderCommand(handler, request);
            _prevCommands.Push(command);
            return await command.Execute();
        }
        
        public async Task<int> Send(FinishOrder request)
        {
            var handler = _serviceProvider.GetService<FinishOrderHandler>();
            var command = new FinishOrderCommand(handler, request);
            _prevCommands.Push(command);
            return await command.Execute();
        }

        public async Task<int> Send(DeleteOrder request)
        {
            var handler = _serviceProvider.GetService<DeleteOrderHandler>();
            var command = new DeleteOrderCommand(handler, request);
            _prevCommands.Push(command);
            return await command.Execute();
        }

        #endregion

        #region Item

        public async Task<int> Send(CreateItems request)
        {
            var handler = _serviceProvider.GetService<CreateItemsHandler>();
            var command = new CreateItemsCommand(handler, request);
            _prevCommands.Push(command);
            return await command.Execute();
        }

        public async Task<int> Send(UpdateItem request)
        {
            var handler = _serviceProvider.GetService<UpdateItemHandler>();
            var command = new UpdateItemCommand(handler, request);
            _prevCommands.Push(command);
            return await command.Execute();
        }

        public async Task<int> Send(DeleteItem request)
        {
            var handler = _serviceProvider.GetService<DeleteItemHandler>();
            var command = new DeleteItemCommand(handler, request);
            _prevCommands.Push(command);
            return await command.Execute();
        }

        #endregion

        public async Task<int> Send(CreateNotification request)
        {
            var handler = _serviceProvider.GetService<AddNotificationHandler>();
            var command = new AddNotificationCommand(handler, request);
            _prevCommands.Push(command);
            return await command.Execute();
        }

        #region Discount
        public async Task<int> Send(CreateDiscount request)
        {
            var handler = _serviceProvider.GetService<CreateDiscountHandler>();
            var command = new CreateDiscountCommand(handler, request);
            _prevCommands.Push(command);
            return await command.Execute();
        }

        public async Task<int> Send(ApplyDiscount request)
        {
            var handler = _serviceProvider.GetService<ApplyDiscountHandler>();
            var command = new ApplyDiscountCommand(handler, request);
            _prevCommands.Push(command);
            return await command.Execute();
        }

        public async Task<int> Send(UpdateDiscount request)
        {
            var handler = _serviceProvider.GetService<UpdateDiscountHandler>();
            var command = new UpdateDiscountCommand(handler, request);
            _prevCommands.Push(command);
            return await command.Execute();
        }

        public async Task<int> Send(DeleteDiscount request)
        {
            var handler = _serviceProvider.GetService<DeleteDiscountHandler>();
            var command = new DeleteDiscountCommand(handler, request);
            _prevCommands.Push(command);
            return await command.Execute();
        }

        #endregion

        public async Task<int> Send<TRequest>(TRequest request)
        {
            var RetryTimes = 3;
            var WaitTime = 1000;
            for(int i=1; i < RetryTimes; i++)
            {
                try
                {
                    return request switch
                    {
                        CreateGuestOrder trequest => await Send(trequest),
                        CreateOrder trequest => await Send(trequest),
                        DeleteOrder trequest => await Send(trequest),
                        UpdateOrder trequest => await Send(trequest),
                        FinishOrder trequest => await Send(trequest),
                        CreateItems trequest => await Send(trequest),
                        UpdateItem trequest => await Send(trequest),
                        DeleteItem trequest => await Send(trequest),
                        CreateDiscount trequest => await Send(trequest),
                        ApplyDiscount trequest => await Send(trequest),
                        DeleteDiscount trequest => await Send(trequest),
                        UpdateDiscount trequest => await Send(trequest),
                        CreateNotification trequest => await Send(trequest),
                        _ => throw new ArgumentException(),
                    };
                }
                catch (ArgumentException ex)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Try number " + i);
                    if (i == 3)
                    {
                        foreach(var command in _prevCommands)
                        {
                            await command.Undo();
                        }
                        _prevCommands.Clear();
                        throw;
                    }
                    await Task.Delay(WaitTime);
                }
            }
            return -1;
        }
    }
}
