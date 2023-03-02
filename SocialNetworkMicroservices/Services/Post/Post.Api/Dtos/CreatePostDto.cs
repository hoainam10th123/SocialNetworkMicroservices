using System.ComponentModel.DataAnnotations;

namespace Post.Api.Dtos
{
    public class CreatePostDto
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string NoiDung { get; set; }
    }
}
