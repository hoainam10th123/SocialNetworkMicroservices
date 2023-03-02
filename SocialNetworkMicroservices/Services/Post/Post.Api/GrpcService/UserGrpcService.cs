using User.Grpc.Protos;

namespace Post.Api.GrpcService
{
    public class UserGrpcService
    {
        private readonly UserProtoService.UserProtoServiceClient _service;

        public UserGrpcService(UserProtoService.UserProtoServiceClient service)
        {
            _service = service;
        }

        public async Task<UserReply> GetUserByUsername(string username)
        {
            var request = new LoginRequest
            {
                UserName = username,
                Password = ""
            };

            return await _service.GetUserByUsernameAsync(request);
        }
    }
}
