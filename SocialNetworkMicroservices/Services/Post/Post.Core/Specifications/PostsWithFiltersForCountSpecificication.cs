

namespace Post.Core.Specifications
{
    public class PostsWithFiltersForCountSpecificication : BaseSpecifcation<Entities.Post>
    {
        public PostsWithFiltersForCountSpecificication(PostSpecParams postParams)
            : base(x => string.IsNullOrEmpty(postParams.Username) || x.Username == postParams.Username)
        {
        }
    }
}
