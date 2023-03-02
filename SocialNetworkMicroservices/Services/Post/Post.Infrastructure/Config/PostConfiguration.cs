using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// xem trong DataContext
namespace Post.Infrastructure.Config
{
    public class PostConfiguration : IEntityTypeConfiguration<Core.Entities.Post>
    {
        public void Configure(EntityTypeBuilder<Core.Entities.Post> builder)
        {
            //Microsoft.EntityFrameworkCore.Relational HasColumnType
            builder.Property(x => x.NoiDung).HasColumnType("ntext");
        }
    }
}
