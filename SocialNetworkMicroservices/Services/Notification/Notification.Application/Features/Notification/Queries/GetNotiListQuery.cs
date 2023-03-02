using MediatR;


namespace Notification.Application.Features.Notification.Queries
{
    public class GetNotiListQuery : IRequest<List<NotificationDto>>
    {
        public string UserName { get; set; }

        public GetNotiListQuery(string userName)
        {
            UserName = userName;
        }
    }
}
