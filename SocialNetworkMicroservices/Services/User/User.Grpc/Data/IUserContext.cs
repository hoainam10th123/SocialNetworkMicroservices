using MongoDB.Driver;

namespace User.Grpc.Data
{
    public interface IUserContext
    {
        IMongoCollection<Entities.User> Users { get; }
    }
}
