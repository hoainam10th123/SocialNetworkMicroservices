using AutoMapper;
using AutoMapper.QueryableExtensions;
using Chat.Api.Data;
using Chat.Api.Dto;
using Chat.Api.Entities;
using Chat.Api.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Chat.Api.Repositories
{
    public interface IMessageRepo
    {
        Task<Pagination<MessageDto>> GetMessageThread(MessageParams messageParams);
        Task<MessageThread> GetMessageThread(string currentUsername, string recipientUsername);
        Task<Group?> GetMessageGroup(string groupName);
        Task<Group?> GetGroupForConnection(string connectionId);
        void AddGroup(Group group);
        void AddMessage(Message mess);
    }

    public class MessageRepo : IMessageRepo
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public MessageRepo(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Pagination<MessageDto>> GetMessageThread(MessageParams messageParams)
        {
            var messages = _context.Messages!
                .Where(m => m.RecipientUsername == messageParams.CurrentUsername && m.SenderUsername == messageParams.RecipientUsername || m.RecipientUsername == messageParams.RecipientUsername && m.SenderUsername == messageParams.CurrentUsername)
                .OrderByDescending(m => m.MessageSent)
                .ProjectTo<MessageDto>(_mapper.ConfigurationProvider)
                .AsQueryable();

            var totalItems = await messages.CountAsync();

            var items = await messages.Skip((messageParams.PageNumber - 1) * messageParams.PageSize).Take(messageParams.PageSize).ToListAsync();            

            return new Pagination<MessageDto>(messageParams.PageNumber, messageParams.PageSize, totalItems, items);
        }

        public async Task<MessageThread> GetMessageThread(string currentUsername, string recipientUsername)
        {
            var messages = _context.Messages
                .Where(m => m.RecipientUsername == currentUsername && m.SenderUsername == recipientUsername || m.RecipientUsername == recipientUsername && m.SenderUsername == currentUsername)
                .OrderByDescending(m => m.MessageSent)
                .ProjectTo<MessageDto>(_mapper.ConfigurationProvider)
                .AsQueryable();

            var totalItems = await messages.CountAsync();

            var pageNumber = 1;
            var pageSize = 24;

            var items = await messages.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return new MessageThread(totalItems, pageSize, items);
        }

        public async Task<Group?> GetMessageGroup(string groupName)
        {
            return await _context.Groups!.Include(x => x.Connections).FirstOrDefaultAsync(x => x.Name == groupName);
        }

        public async Task<Group?> GetGroupForConnection(string connectionId)
        {
            return await _context.Groups!.Include(x => x.Connections)
                .Where(x => x.Connections.Any(c => c.ConnectionId == connectionId))
                .FirstOrDefaultAsync();
        }

        public void AddGroup (Group group)
        {
            _context.Groups.Add(group);
        }

        public void AddMessage(Message mess)
        {
            _context.Messages.Add(mess);
        }
    }
}
