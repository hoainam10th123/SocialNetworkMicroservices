using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GeoJsonObjectModel;
using User.Grpc.Data;
using User.Grpc.Helpers;
using User.Grpc.Protos;

namespace User.Grpc.Services
{
    public class UserService : UserProtoService.UserProtoServiceBase
    {
        private readonly ILogger<UserService> _logger;
        private readonly IUserContext _context;
        const int unitValue = 1000;
        private readonly IConfiguration _config;

        public UserService(ILogger<UserService> logger, IUserContext context, IConfiguration config)
        {
            _logger = logger;     
            _context = context;
            _config = config;
        }        

        public override async Task<Empty> AddUser(UserRequest request, ServerCallContext context)
        {
            var mainUser = await _context.Users.Find(x => x.UserName == request.UserName).FirstOrDefaultAsync();
            if(mainUser == null)
            {
                var passwordHash = Helper.CreateMD5(request.Password);
                var entity = new Entities.User
                {
                    UserName = request.UserName,
                    PasswordHash = passwordHash,
                    SubjectId = request.UserName,
                    ImageUrl = "/images/user.png",
                    Location = GeoJson.Point(GeoJson.Geographic(request.Long, request.Lat))
                };
                await _context.Users.InsertOneAsync(entity);
                _logger.LogInformation($"Insert success {request.UserName}");
            }
            else
            {
                _logger.LogInformation($"{request.UserName} has already exist!");
            }
            
            return new Empty();
        }

        public override async Task<UserReply> Login(LoginRequest request, ServerCallContext context)
        {
            var mainUser = await _context.Users.Find(x => x.UserName == request.UserName).FirstOrDefaultAsync();
            if(mainUser != null)
            {
                var md5Hash = Helper.CreateMD5(request.Password);
                if(mainUser.PasswordHash == md5Hash)
                {
                    _logger.LogInformation($"{request.UserName} login success");
                    return new UserReply { 
                        UserName = mainUser.UserName, 
                        SubjectId = mainUser.SubjectId, 
                        ImageUrl = $"{_config["BaseUrl:UserApiUrl"]}{mainUser.ImageUrl}"
                    };
                }
            }
            return new UserReply { UserName = "", SubjectId = "", ImageUrl = "" };
        }

        public override async Task<UserReply> GetUserByUsername(LoginRequest request, ServerCallContext context)
        {
            var mainUser = await _context.Users.Find(x => x.UserName == request.UserName).FirstOrDefaultAsync();
            if (mainUser != null)
            {
                _logger.LogInformation($"{request.UserName} is get success");
                return new UserReply { 
                    UserName = mainUser.UserName, 
                    SubjectId = mainUser.SubjectId,
                    ImageUrl = $"{mainUser.ImageUrl}",
                };
            }
            return new UserReply { UserName = "", SubjectId = "", ImageUrl = "" };
        }

        public override async Task<UsersReply> FindNearest(Location request, ServerCallContext context)
        {
            const int distance = 20;// 20 km radius            

            BsonDocument geoNear = new BsonDocument{
                {
                    "$geoNear", new BsonDocument{
                        {
                            "near", new BsonDocument
                            {
                                { "type", "Point" },
                                { "coordinates", new BsonArray(new [] { request.Long, request.Lat }) }
                            }

                        },
                        { "maxDistance",  distance*unitValue },
                        { "distanceField","distance" },
                        { "distanceMultiplier",  (float)1 / unitValue },
                    }
                }
            };

            BsonDocument project = new BsonDocument{
                {
                    "$project", new BsonDocument{
                        { "UserName", 1 },
                        { "distance", 1 },
                        { "ImageUrl", 1 },
                    }
                }
            };

            BsonDocument sort = new BsonDocument{
                {
                    "$sort", new BsonDocument{
                        { "distance", 1 }
                    }
                }
            };

            BsonDocument limit = new BsonDocument{
                {
                    "$limit", 10
                }
            };

            BsonDocument[] pipeline = new BsonDocument[] {
                geoNear,
                project,
                sort,
                limit
            };

            List<BsonDocument> pResults = await _context.Users.Aggregate<BsonDocument>(pipeline).ToListAsync();
            var list = new UsersReply();

            foreach (BsonDocument pResult in pResults)
            {
                var user = new UserLocation
                {
                    UserName = pResult.GetValue("UserName").ToString(),
                    Distance = ConvertToMeter(pResult.GetValue("distance").ToDouble()),
                    ImageUrl = $"{pResult.GetValue("ImageUrl")}"
                };
                list.ResultUsers.Add(user);
            }
            return list;
        }

        private string ConvertToMeter(double value)
        {
            if (value < 1)
            {
                var tempValue = value * unitValue;
                return $"{Math.Round(tempValue, 1)} m";
            }
            return $"{Math.Round(value, 1)} km";
        }
    }
}
