using AutoMapper;
using Chat.Api.Dto;
using Chat.Api.Entities;

namespace Chat.Api.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<Message, MessageDto>();
        }
    }
}
