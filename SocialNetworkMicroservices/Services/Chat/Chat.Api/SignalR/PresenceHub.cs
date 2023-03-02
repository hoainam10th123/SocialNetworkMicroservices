using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace Chat.Api.SignalR
{
    [Authorize]
    public class PresenceHub : Hub
    {
        private readonly PresenceTracker _tracker;

        public PresenceHub(PresenceTracker tracker)
        {
            _tracker = tracker;
        }

        public override async Task OnConnectedAsync()
        {
            var username = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var isOnline = await _tracker.UserConnected(username, Context.ConnectionId);
            if (isOnline)
            {
                await Clients.Others.SendAsync("UserIsOnline", username);
            }
            var currentUsers = await _tracker.GetOnlineUsers();            
            await Clients.Caller.SendAsync("GetOnlineUsers", currentUsers.Where(x => x != username).ToArray());
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var username = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var isOffline = await _tracker.UserDisconnected(username, Context.ConnectionId);
            if (isOffline)
            {
                await Clients.Others.SendAsync("UserIsOffline", username);
            }
            await base.OnDisconnectedAsync(exception);
        }
    }
}
