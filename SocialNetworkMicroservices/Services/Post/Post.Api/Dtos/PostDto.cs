

namespace Post.Api.Dtos
{
    public class PostDto
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string Title { get; set; }
        public string NoiDung { get; set; }
        public string Username { get; set; }
        public string UserPhotoUrl { get; set; }
        public ICollection<CommentDto> Comments { get; set; }
    }
}
