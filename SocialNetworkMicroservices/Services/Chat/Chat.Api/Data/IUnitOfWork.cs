using AutoMapper;
using Chat.Api.Repositories;

namespace Chat.Api.Data
{
    public interface IUnitOfWork : IDisposable
    {
        IMessageRepo MessageRepository { get; }
        Task<bool> Complete();
        bool HasChanges();
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;        
        private readonly IMapper _mapper;

        public UnitOfWork(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IMessageRepo MessageRepository => new MessageRepo(_context, _mapper);

        public async Task<bool> Complete()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public bool HasChanges()
        {
            return _context.ChangeTracker.HasChanges();
        }
    }
}
