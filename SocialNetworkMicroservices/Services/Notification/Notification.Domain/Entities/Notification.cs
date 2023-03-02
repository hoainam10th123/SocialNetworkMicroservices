using System.ComponentModel.DataAnnotations;

namespace Notification.Domain.Entities
{

    public class Notification
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string NoiDung { get; set; } = null!;
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public string Username { get; set; } = null!;// noti cua user nao
        public string UsernameComment { get; set; } = null!;
        public string? PostId { get; set; }
        public string? CommentId { get; set; }
    }
}
