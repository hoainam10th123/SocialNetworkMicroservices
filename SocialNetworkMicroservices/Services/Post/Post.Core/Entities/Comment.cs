

namespace Post.Core.Entities
{
    public class Comment : BaseEntity
    {
        public string NoiDung { get; set; }
        public string Username { get; set; }
        public Post Post { get; set; }
        public Guid PostId { get; set; }
    }
}
