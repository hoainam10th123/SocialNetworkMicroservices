

namespace Notification.Application.Features.Notification.Queries
{
    public class NotificationDto
    {
        public Guid Id { get; set; }
        public string NoiDung { get; set; }
        public DateTime DateCreated { get; set; }
        public string Username { get; set; }
        public string UsernameComment { get; set; }
        public string PostId { get; set; }
        public string CommentId { get; set; }
    }
}
