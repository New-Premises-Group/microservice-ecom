using IW.Common;
using IW.Handlers.Notifications;
using IW.Interfaces.Commands;
using IW.Models.DTOs;

namespace IW.Commands.Notifications
{
    public class AddNotificationCommand : GenericCommand, ICommand<CreateNotification>
    {
        public AddNotificationCommand(
            AddNotificationHandler handler,
            CreateNotification request) : base(handler, request) { }
    }
}
