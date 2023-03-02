

namespace Notification.Application.Contracts.Persistence
{
    public interface INotiRepository : IAsyncRepository<Domain.Entities.Notification>
    {
        Task<IEnumerable<Domain.Entities.Notification>> GetNotificationsByUserName(string userName);
    }
}
