

namespace Post.Core.Specifications
{
    public class CommentSpecification : BaseSpecifcation<Entities.Comment>
    {
        public CommentSpecification(CommentSpecParams postParams)
            : base(x =>
            string.IsNullOrEmpty(postParams.Username) || x.Username == postParams.Username
        )
        {
            ApplyPaging(postParams.PageSize * (postParams.PageNumber - 1), postParams.PageSize);
        }

        public CommentSpecification(Guid id)
            : base(x => x.Id == id)
        {
        }
    }
}
