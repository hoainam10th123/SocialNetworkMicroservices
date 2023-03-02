using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using User.Api.Dtos;
using User.Api.GrpcService;

namespace User.Api.Controllers
{    
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserGrpcService _userGrpcService;
        public UserController(UserGrpcService userGrpcService) 
        { 
            _userGrpcService = userGrpcService;
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(UserDto userDto)
        {
            await _userGrpcService.AddUser(userDto);
            return NoContent();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto userDto)
        {
            var model = await _userGrpcService.Login(userDto);
            return Ok(model);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> FindNearest(double lng, double lat)
        {
            var model = await _userGrpcService.FindNearest(lng, lat);
            return Ok(model);
        }
    }
}
