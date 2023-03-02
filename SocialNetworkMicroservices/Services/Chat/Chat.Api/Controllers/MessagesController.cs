using Chat.Api.Data;
using Chat.Api.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public MessagesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetMessages([FromQuery] MessageParams messParams)
        {
            var data = await _unitOfWork.MessageRepository.GetMessageThread(messParams);
            return Ok(data);
        }
    }
}
