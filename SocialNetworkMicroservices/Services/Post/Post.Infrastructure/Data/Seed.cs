

using Microsoft.EntityFrameworkCore;

namespace Post.Infrastructure.Data
{
    public class Seed
    {
        public static async Task SeedPostAsync(DataContext context)
        {
            if (!await context.Posts.AnyAsync())
            {
                var posts = new List<Core.Entities.Post>
                {
                    new Core.Entities.Post
                    {
                        Id = Guid.NewGuid(),
                        Title = "Title 1",
                        NoiDung = "Bài viết số 1",
                        Username= "hoainam10th",                        
                    },
                    new Core.Entities.Post
                    {
                        Id = Guid.NewGuid(),
                        Title = "Title 2",
                        NoiDung = "Bài viết số 2",
                        Username= "hoainam10th",
                    },
                    new Core.Entities.Post
                    {
                        Id = Guid.NewGuid(),
                        Title = "Title 3",
                        NoiDung = "Bài viết số 3",
                        Username= "ubuntu",
                    },
                    new Core.Entities.Post
                    {
                        Id = Guid.NewGuid(),
                        Title = "Title 4",
                        NoiDung = "Bài viết số 4",
                        Username= "ubuntu",
                    },
                    new Core.Entities.Post
                    {
                        Id = Guid.NewGuid(),
                        Title = "Title 5",
                        NoiDung = "Bài viết số 5",
                        Username= "datnguyen",
                    },
                    new Core.Entities.Post
                    {
                        Id = Guid.NewGuid(),
                        Title = "Title 6",
                        NoiDung = "Bài viết số 6",
                        Username= "datnguyen",
                    },
                    new Core.Entities.Post
                    {
                        Id = Guid.NewGuid(),
                        Title = "Title 7",
                        NoiDung = "Bài viết số 7",
                        Username= "datnguyen",
                    },
                    new Core.Entities.Post
                    {
                        Id = Guid.NewGuid(),
                        Title = "Title 8",
                        NoiDung = "Bài viết số 8",
                        Username= "ubuntu",
                    },
                    new Core.Entities.Post
                    {
                        Id = Guid.NewGuid(),
                        Title = "Title 9",
                        NoiDung = "Bài viết số 9",
                        Username= "hoainam10th",
                    },
                    new Core.Entities.Post
                    {
                        Id = Guid.NewGuid(),
                        Title = "Title 10",
                        NoiDung = "Bài viết số 10",
                        Username= "hoainam10th",
                    },
                    new Core.Entities.Post
                    {
                        Id = Guid.NewGuid(),
                        Title = "Title 11",
                        NoiDung = "Bài viết số 11",
                        Username= "ubuntu",
                    },
                };
                context.Posts.AddRange(posts);
                await context.SaveChangesAsync();
            }
        }
    }
}
