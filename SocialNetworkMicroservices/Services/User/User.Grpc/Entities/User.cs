using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using MongoDB.Driver.GeoJsonObjectModel;

namespace User.Grpc.Entities
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string UserName { get; set; }     
        public string PasswordHash { get; set; }
        public string SubjectId { get; set; }
        public string ImageUrl { get; set; }
        public GeoJsonPoint<GeoJson2DGeographicCoordinates> Location { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
