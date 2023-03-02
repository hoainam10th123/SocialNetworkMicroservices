using IdentityServer.Dtos;
using User.Grpc.Protos;

namespace IdentityServer.GrpcService
{
    public class UserGrpcService
    {
        private readonly UserProtoService.UserProtoServiceClient _service;

        public UserGrpcService(UserProtoService.UserProtoServiceClient service)
        {
            _service = service;
        }

        public async Task<UserReply> Login(string username, string password)
        {
            var request = new LoginRequest
            {
                UserName = username,
                Password = password
            };

            return await _service.LoginAsync(request);
        }
    }
}
