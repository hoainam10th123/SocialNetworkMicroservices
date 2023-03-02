using System.ComponentModel.DataAnnotations;

namespace Chat.Api.Entities
{
    public class Message
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string SenderUsername { get; set; } = null!;
        public string RecipientUsername { get; set; } = null!;
        public string Content { get; set; } = null!;
        public DateTime MessageSent { get; set; } = DateTime.UtcNow;
    }
}
