using MediatR;


namespace Notification.Application.Features.Notification.Commands.AddNoti
{
    public class AddNotiCommand : IRequest<string>
    {
        public string NoiDung { get; set; }
        public string Username { get; set; }
        public string UsernameComment { get; set; }
        public string PostId { get; set; }
        public string CommentId { get; set; }
    }
}
