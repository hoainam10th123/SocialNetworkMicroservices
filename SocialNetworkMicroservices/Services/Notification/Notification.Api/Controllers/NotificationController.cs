using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notification.Api.Services;
using Notification.Application.Features.Notification.Commands.AddNoti;
using Notification.Application.Features.Notification.Queries;
using System.Net;

namespace Notification.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IIdentityService _identityService;

        public NotificationController(IMediator mediator, IIdentityService identityService)
        {
            _mediator = mediator;
            _identityService = identityService;
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<string>> AddNotification([FromBody] AddNotiCommand command)
        {
            var result = await _mediator.Send(command);
            // result = 2, id trong db = 2
            return Ok(new {id = result });
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<NotificationDto>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<NotificationDto>>> GetNotifications()
        {
            var userName = _identityService.GetUserIdentity();
            var query = new GetNotiListQuery(userName);
            var orders = await _mediator.Send(query);
            return Ok(orders);
        }
    }
}
