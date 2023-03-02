using AutoMapper;
using MediatR;
using Notification.Application.Contracts.Persistence;

namespace Notification.Application.Features.Notification.Queries
{
    public class GetNotiListQueryHandler : IRequestHandler<GetNotiListQuery, List<NotificationDto>>
    {
        private readonly IMapper _mapper;
        private readonly INotiRepository _notiRepository;

        public GetNotiListQueryHandler(INotiRepository notiRepository, IMapper mapper)
        {
            _mapper = mapper;
            _notiRepository = notiRepository;
        }

        public async Task<List<NotificationDto>> Handle(GetNotiListQuery request, CancellationToken cancellationToken)
        {
            var notiList = await _notiRepository.GetNotificationsByUserName(request.UserName);
            return _mapper.Map<List<NotificationDto>>(notiList);
        }
    }
}
