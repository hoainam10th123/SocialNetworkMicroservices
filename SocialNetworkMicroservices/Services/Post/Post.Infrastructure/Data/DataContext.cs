using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Post.Infrastructure.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options) { }

        public DbSet<Core.Entities.Comment> Comments { get; set; }
        public DbSet<Core.Entities.Post> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // apply tat ca cac config fluent api
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
