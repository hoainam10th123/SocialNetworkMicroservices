

namespace Post.Core.Entities
{
    public class Post : BaseEntity
    {
        public string Title { get; set; }
        public string NoiDung { get; set; }
        public string Username { get; set; }
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
