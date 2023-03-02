using Chat.Api.Dto;
using Chat.Api.SignalR;
using EventBus.Messages.Events;
using MassTransit;
using Microsoft.AspNetCore.SignalR;

namespace Chat.Api.EventBusConsumer
{
    public class NotificationConsumer : IConsumer<NotificationEvent>
    {
        private readonly ILogger<NotificationConsumer> _logger;
        private readonly IHubContext<PresenceHub> _presenceHub;
        private readonly PresenceTracker _presenceTracker;

        public NotificationConsumer(IHubContext<PresenceHub> presenceHub, 
            PresenceTracker presenceTracker, 
            ILogger<NotificationConsumer> logger) 
        {
            _logger = logger;
            _presenceTracker = presenceTracker;
            _presenceHub = presenceHub;
        }

        // tu chay khi dang ky thanh cong o program.cs
        public async Task Consume(ConsumeContext<NotificationEvent> context)
        {
            var connections = await _presenceTracker.GetConnectionsForUser(context.Message.UserNameNhan);
            if (connections != null)
            {
                await _presenceHub.Clients.Clients(connections).SendAsync("NewNotificationReceived", context.Message.UserNameComment);
                _logger.LogInformation("NotificationEvent consumed successfully. Id : {Id}", context.Message.Id.ToString());
            }
            else
            {
                _logger.LogInformation("NotificationEvent consumed No message send. Id : {Id}", context.Message.Id.ToString());
            }            
        }
    }
}
