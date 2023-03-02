using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Post.Api.Dtos;
using Post.Api.Errors;
using Post.Api.GrpcService;
using Post.Api.Helpers;
using Post.Api.Services;
using Post.Core.Interfaces;
using Post.Core.Specifications;


namespace Post.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        //private readonly IGenericRepository<Core.Entities.Post> _postsRepo;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserGrpcService _grpcService;
        private readonly IIdentityService _identityService;

        public PostsController(IUnitOfWork unitOfWork, IMapper mapper, UserGrpcService grpcService, IIdentityService identityService) 
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _grpcService = grpcService;
            _identityService = identityService;
        }

        [HttpPost]
        public async Task<ActionResult> Post(CreatePostDto createPost)
        {
            var userId = _identityService.GetUserIdentity();
            if (string.IsNullOrEmpty(userId)) return BadRequest(new ApiResponse(400, "Username is Null"));

            var post = _mapper.Map<Core.Entities.Post>(createPost);
            post.Id = Guid.NewGuid();
            post.Username = userId;

            _unitOfWork.Repository<Core.Entities.Post>().Add(post);

            if (await _unitOfWork.Complete())
            {

                var spec = new PostsSpecification(post.Id);
                var postDb = await _unitOfWork.Repository<Core.Entities.Post>().GetEntityWithSpec(spec);
                var postDto = _mapper.Map<Core.Entities.Post, PostDto>(postDb);
                
                var user = await _grpcService.GetUserByUsername(userId);
                postDto.UserPhotoUrl = user.ImageUrl;

                return Ok(postDto);
            }
            return BadRequest(new ApiResponse(400, "Can not add post"));
        }

        [HttpGet]
        public async Task<IActionResult> GetPosts([FromQuery] PostSpecParams postParams)
        {
            var spec = new PostsSpecification(postParams);

            var countSpec = new PostsWithFiltersForCountSpecificication(postParams);

            var totalItems = await _unitOfWork.Repository<Core.Entities.Post>().CountAsync(countSpec);

            var products = await _unitOfWork.Repository<Core.Entities.Post>().ListAsync(spec);

            var data = _mapper.Map<IReadOnlyList<PostDto>>(products);

            foreach(var post in data)
            {
                var user = await _grpcService.GetUserByUsername(post.Username);
                post.UserPhotoUrl = user.ImageUrl;
            }

            return Ok(new Pagination<PostDto>(postParams.PageNumber, postParams.PageSize, totalItems, data));
        }

        [HttpGet("{id}", Name = "Detail")]
        public async Task<ActionResult<PostDto>> Detail(Guid id)
        {
            var spec = new PostsSpecification(id);
            var post = await _unitOfWork.Repository<Core.Entities.Post>().GetEntityWithSpec(spec);

            if (post == null) return NotFound(new ApiResponse(404, "Can not find post with given id"));

            var mapToPostDto = _mapper.Map<Core.Entities.Post, PostDto>(post);            

            return Ok(mapToPostDto);
        }

        [Authorize(Policy = "IsPostOwner")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(Guid id)
        {
            var postDb = await _unitOfWork.Repository<Core.Entities.Post>().GetByIdAsync(id);
            if (postDb != null)
            {
                _unitOfWork.Repository<Core.Entities.Post>().Delete(postDb);
                if (await _unitOfWork.Complete()) return Ok(new { id = postDb.Id });
                return BadRequest(new ApiResponse(400, "Delete post failed"));
            }
            return NotFound(new ApiResponse(404, "Post is not found"));
        }
    }
}
