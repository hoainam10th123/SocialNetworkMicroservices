using AutoMapper;
using EventBus.Messages.Events;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Post.Api.Dtos;
using Post.Api.Errors;
using Post.Api.GrpcService;
using Post.Api.Services;
using Post.Core.Interfaces;
using Post.Core.Specifications;

namespace Post.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserGrpcService _grpcService;
        private readonly IIdentityService _identityService;        
        private readonly IPublishEndpoint _publishEndpoint;//rabbitmq

        public CommentsController(IUnitOfWork unitOfWork, 
            IMapper mapper, 
            UserGrpcService grpcService, 
            IIdentityService identityService, 
            IPublishEndpoint publishEndpoint)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _grpcService = grpcService;
            _identityService = identityService;
            _publishEndpoint = publishEndpoint;
        }


        [HttpPost]
        public async Task<ActionResult> Post(CreateCommentDto createPost)
        {
            var userId = _identityService.GetUserIdentity();
            if (string.IsNullOrEmpty(userId)) return BadRequest(new ApiResponse(400, "Username is Null"));

            var post = _mapper.Map<Core.Entities.Comment>(createPost);
            post.Id = Guid.NewGuid();
            post.Username = userId;            

            _unitOfWork.Repository<Core.Entities.Comment>().Add(post);
            
            if (await _unitOfWork.Complete())
            {
                // pub to chat.api and notification.api
                await _publishEndpoint.Publish(new NotificationEvent
                {
                    UserNameNhan = createPost.UserNameNhan,
                    UserNameComment = userId,
                    Message = $"{userId} comment into your post",
                    PostId = createPost.PostId.ToString()
                });

                var spec = new CommentSpecification(post.Id);
                var postDb = await _unitOfWork.Repository<Core.Entities.Comment>().GetEntityWithSpec(spec);
                var postDto = _mapper.Map<CommentDto>(postDb);

                var user = await _grpcService.GetUserByUsername(userId);
                postDto.UserPhotoUrl = user.ImageUrl;

                return Ok(postDto);
            }
            return BadRequest(new ApiResponse(400, "Can not add comment"));
        }

        [Authorize(Policy = "IsCommentOwner")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(Guid id)
        {
            var postDb = await _unitOfWork.Repository<Core.Entities.Comment>().GetByIdAsync(id);
            if (postDb != null)
            {
                _unitOfWork.Repository<Core.Entities.Comment>().Delete(postDb);
                if (await _unitOfWork.Complete()) return Ok(new { id = postDb.Id });
                return BadRequest(new ApiResponse(400, "Delete comment failed"));
            }
            return NotFound(new ApiResponse(404, "comment is null, can not delete"));
        }
    }
}
