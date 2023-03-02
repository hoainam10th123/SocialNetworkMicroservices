using Microsoft.Extensions.Options;
using MongoDB.Driver;
using User.Grpc.Dtos;

namespace User.Grpc.Data
{
    public class UserContext : IUserContext
    {
        public UserContext(IOptions<DatabaseSettings> userStoreDatabaseSettings) 
        {
            var mongoClient = new MongoClient(userStoreDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(userStoreDatabaseSettings.Value.DatabaseName);

            Users = mongoDatabase.GetCollection<Entities.User>(userStoreDatabaseSettings.Value.UsersCollectionName);

            Users.Indexes.CreateManyAsync(new[] { new CreateIndexModel<Entities.User>(Builders<Entities.User>.IndexKeys.Geo2DSphere(it => it.Location)) });
        }

        public IMongoCollection<Entities.User> Users { get; }
    }
}
