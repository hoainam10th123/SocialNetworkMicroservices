

namespace Post.Core.Specifications
{
    public class PostsSpecification : BaseSpecifcation<Entities.Post>
    {
        public PostsSpecification(PostSpecParams postParams)
            : base(x =>            
            string.IsNullOrEmpty(postParams.Username) || x.Username == postParams.Username
        )
        {
            AddOrderByDescending(x => x.CreatedDate);
            ApplyPaging(postParams.PageSize * (postParams.PageNumber - 1), postParams.PageSize);
            AddInclude(x => x.Comments);
        }

        public PostsSpecification(Guid id)
            : base(x => x.Id == id)
        {
        }
    }
}
