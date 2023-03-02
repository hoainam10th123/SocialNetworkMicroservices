using AutoMapper;
using Post.Api.Dtos;

namespace Post.Api.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<Core.Entities.Comment, CommentDto>();
            CreateMap<Core.Entities.Post, PostDto>();
            CreateMap<CreatePostDto, Core.Entities.Post>();
            CreateMap<CreateCommentDto, Core.Entities.Comment>();
        }
    }
}
