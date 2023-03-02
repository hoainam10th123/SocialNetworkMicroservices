namespace Chat.Api.Dto
{
    public class MessageDto
    {
        public Guid Id { get; set; }
        public string SenderUsername { get; set; } = null!;
        public string RecipientUsername { get; set; } = null!;
        public string Content { get; set; } = null!;
        public DateTime MessageSent { get; set; }
    }
}
