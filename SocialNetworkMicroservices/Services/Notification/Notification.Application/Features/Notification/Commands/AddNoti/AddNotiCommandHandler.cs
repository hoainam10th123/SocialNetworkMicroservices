using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Notification.Application.Contracts.Persistence;


namespace Notification.Application.Features.Notification.Commands.AddNoti
{
    public class AddNotiCommandHandler : IRequestHandler<AddNotiCommand, string>
    {
        private readonly INotiRepository _notiRepository;
        private readonly IMapper _mapper;
        //package: Microsoft.Extensions.Logging.Abstractions
        private readonly ILogger<AddNotiCommandHandler> _logger;

        public AddNotiCommandHandler(INotiRepository notiRepository, IMapper mapper, ILogger<AddNotiCommandHandler> logger) 
        {
            _notiRepository = notiRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<string> Handle(AddNotiCommand request, CancellationToken cancellationToken)
        {
            var orderEntity = _mapper.Map<Domain.Entities.Notification>(request);
            var newOrder = await _notiRepository.AddAsync(orderEntity);

            _logger.LogInformation($"Notification {newOrder.Id} is successfully created.");

            return newOrder.Id.ToString();
        }
    }
}
