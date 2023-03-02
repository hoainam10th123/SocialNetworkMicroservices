namespace Chat.Api.Dto
{
    public class CreateMessageDto
    {
        public string? RecipientUsername { get; set; }
        public string? Content { get; set; }
    }
}
