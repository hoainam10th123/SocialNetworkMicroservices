using MongoDB.Driver;
using MongoDB.Driver.GeoJsonObjectModel;
using User.Grpc.Helpers;

namespace User.Grpc.Data
{
    public class Seed
    {
        public static async Task SeedData(IMongoCollection<Entities.User> userCollection, IConfiguration config)
        {
            bool existProduct = userCollection.Find(p => true).Any();
            if (!existProduct)
            {
                await userCollection.InsertManyAsync(new List<Entities.User>
                {
                    new Entities.User
                    {
                        UserName = "hoainam10th",
                        PasswordHash = Helper.CreateMD5("123456"),
                        Location = GeoJson.Point(GeoJson.Geographic(79.821602, 28.626137)),
                        SubjectId = "hoainam10th",
                        ImageUrl = "/images/user.png"
                    },
                    new Entities.User
                    {
                        UserName = "ubuntu",
                        PasswordHash = Helper.CreateMD5("123456"),
                        SubjectId = "ubuntu",
                        Location = GeoJson.Point(GeoJson.Geographic(79.821091, 28.625484)),
                        ImageUrl = "/images/netcore.png"
                    },
                    new Entities.User
                    {
                        UserName = "datnguyen",
                        PasswordHash = Helper.CreateMD5("123456"),
                        SubjectId = "datnguyen",
                        Location = GeoJson.Point(GeoJson.Geographic(79.817924, 28.625294)),
                        ImageUrl = "/images/user.png"
                    },
                    new Entities.User
                    {
                        UserName = "lina",
                        PasswordHash = Helper.CreateMD5("123456"),
                        SubjectId = "lina",
                        Location = GeoJson.Point(GeoJson.Geographic(79.814636, 28.625181)),
                        ImageUrl = "/images/user.png"
                    },
                    new Entities.User
                    {
                        Location = GeoJson.Point(GeoJson.Geographic(79.810135, 28.625044)),
                        UserName = "phatnguyen",
                        PasswordHash = Helper.CreateMD5("123456"),
                        SubjectId = "phatnguyen",
                        ImageUrl = "/images/user.png"
                    },
                    new Entities.User
                    {
                        Location = GeoJson.Point(GeoJson.Geographic(79.808296, 28.625019)),
                        UserName = "teonguyen",
                        PasswordHash = Helper.CreateMD5("123456"),
                        SubjectId = "teonguyen",
                        ImageUrl = "/images/netcore.png"
                    },
                });
            }
        }


    }
}
