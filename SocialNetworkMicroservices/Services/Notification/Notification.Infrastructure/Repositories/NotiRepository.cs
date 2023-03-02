using Microsoft.EntityFrameworkCore;
using Notification.Application.Contracts.Persistence;
using Notification.Infrastructure.Persistence;

namespace Notification.Infrastructure.Repositories
{
    public class NotiRepository : RepositoryBase<Domain.Entities.Notification>, INotiRepository
    {
        public NotiRepository(DataContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Domain.Entities.Notification>> GetNotificationsByUserName(string userName)
        {
            var listNoti = await _dbContext.Notifications.Where(x => x.Username == userName).ToListAsync();
            return listNoti;
        }
    }
}
