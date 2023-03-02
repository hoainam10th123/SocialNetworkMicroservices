using User.Api.Dtos;
using User.Grpc.Protos;

namespace User.Api.GrpcService
{
    public class UserGrpcService
    {
        private readonly UserProtoService.UserProtoServiceClient _service;

        public UserGrpcService(UserProtoService.UserProtoServiceClient service)
        {
            _service = service;
        }

        public async Task AddUser(UserDto userDto)
        {
            var request = new UserRequest { 
                UserName= userDto.UserName,
                Password= userDto.Password,
                Long = userDto.Long,
                Lat= userDto.Lat
            };

            await _service.AddUserAsync(request);
        }

        public async Task<UserReply> Login(LoginDto login)
        {
            var request = new LoginRequest
            {
                UserName = login.Username,
                Password = login.Password
            };

            return await _service.LoginAsync(request);
        }

        public async Task<UsersReply> FindNearest(double lng, double lat)
        {
            var request = new Location
            {
                Long= lng,
                Lat = lat
            };

            return await _service.FindNearestAsync(request);
        }
    }
}
