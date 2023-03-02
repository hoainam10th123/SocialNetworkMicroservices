using AutoMapper;
using Notification.Application.Features.Notification.Commands.AddNoti;
using Notification.Application.Features.Notification.Queries;

namespace Notification.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Domain.Entities.Notification, NotificationDto>();
            CreateMap<AddNotiCommand, Domain.Entities.Notification>();
        }
    }
}
