namespace Post.Api.Dtos
{
    public class CreateCommentDto
    {
        public string NoiDung { get; set; }
        public Guid PostId { get; set; }
        public string UserNameNhan { get; set; }
    }
}
