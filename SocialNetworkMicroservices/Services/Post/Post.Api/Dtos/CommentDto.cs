namespace Post.Api.Dtos
{
    public class CommentDto
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string NoiDung { get; set; }
        public string Username { get; set; }
        public Guid PostId { get; set; }
        public string UserPhotoUrl { get; set; }
    }
}
