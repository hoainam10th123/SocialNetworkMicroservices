using AutoMapper;
using Chat.Api.Data;
using Chat.Api.Dto;
using Chat.Api.Entities;
using Chat.Api.Services;
using Microsoft.AspNetCore.SignalR;

namespace Chat.Api.SignalR
{
    public class MessageHub : Hub
    {
        private readonly IHubContext<PresenceHub> _presenceHub;
        private readonly PresenceTracker _presenceTracker;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IIdentityService _identityService;
        private readonly IMapper _mapper;

        public MessageHub(IHubContext<PresenceHub> presenceHub, 
            PresenceTracker presenceTracker,
            IUnitOfWork unitOfWork, 
            IIdentityService identityService, IMapper mapper) 
        {
            _presenceHub = presenceHub;
            _presenceTracker = presenceTracker;
            _unitOfWork = unitOfWork;
            _identityService = identityService;
            _mapper = mapper;
        }

        public override async Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();
            var otherUser = httpContext!.Request.Query["user"].ToString();
            var groupName = Helpers.HelpUliti.GetGroupName(_identityService.GetUserIdentity(), otherUser);

            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            await AddToGroup(groupName);
            //mac dinh lay 24 tin nhan
            var messagesThread = await _unitOfWork.MessageRepository.GetMessageThread(_identityService.GetUserIdentity(), otherUser);                        

            await Clients.Caller.SendAsync("ReceiveMessageThread", messagesThread.Messages);
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var group = await RemoveFromMessageGroup();
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, group.Name);
            await base.OnDisconnectedAsync(exception);
        }

        private async Task<Group> AddToGroup(string groupName)
        {
            var group = await _unitOfWork.MessageRepository.GetMessageGroup(groupName);
            var connection = new Connection(Context.ConnectionId, _identityService.GetUserIdentity());
            if (group == null)
            {
                group = new Group(groupName);
                _unitOfWork.MessageRepository.AddGroup(group);
            }
            group.Connections.Add(connection);

            if (await _unitOfWork.Complete()) return group;

            throw new HubException("Failed to join group");
        }

        public async Task SendMessage(CreateMessageDto createMessageDto)
        {
            var senderUserName = _identityService.GetUserIdentity();
            if (senderUserName == createMessageDto.RecipientUsername!.ToLower())
                throw new HubException("You cannot send message to yourself");

            var message = new Message
            {
                SenderUsername = senderUserName,
                RecipientUsername = createMessageDto.RecipientUsername!.ToLower(),
                Content = createMessageDto.Content!
            };

            var groupName = Helpers.HelpUliti.GetGroupName(senderUserName, createMessageDto.RecipientUsername!.ToLower());

            var connections = await _presenceTracker.GetConnectionsForUser(createMessageDto.RecipientUsername!.ToLower());
            if (connections != null)
            {
                await _presenceHub.Clients.Clients(connections).SendAsync("NewMessageReceived", senderUserName);
            }

            _unitOfWork.MessageRepository.AddMessage(message);

            try
            {
                if (await _unitOfWork.Complete())
                {
                    await Clients.Group(groupName).SendAsync("NewMessage", _mapper.Map<MessageDto>(message));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }            
        }

        private async Task<Group> RemoveFromMessageGroup()
        {
            var group = await _unitOfWork.MessageRepository.GetGroupForConnection(Context.ConnectionId);
            var connection = group!.Connections.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
            group.Connections.Remove(connection!);

            if (await _unitOfWork.Complete()) return group;

            throw new HubException("Fail to remove from group");
        }
    }
}
