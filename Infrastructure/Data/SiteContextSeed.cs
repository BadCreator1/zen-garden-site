using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data
{
    public class SiteContextSeed
    {
        public static async Task SeedAsync(SiteDbContext context, ILoggerFactory factory)
        {
            try
            {
                if (!context.BlockTypes.Any())
                {
                    var blockTypes = new List<BlockType>{
                        new BlockType{ Id = 1, Description = "Text" },
                        new BlockType{ Id = 2, Description = "Image" }
                    };
                    foreach (var item in blockTypes)
                    {
                        context.BlockTypes.Add(item);
                    }
                    await context.SaveChangesAsync();
                }

                if (!context.Posts.Any())
                {
                    var posts = new List<Post>{
                        new Post{ Id = 1, DateCreated = DateTime.Now, Title = "Test title", ImageUrl = "images/blank.png" }
                        ,new Post{ Id = 2, DateCreated = DateTime.Now, Title = "Finding concepts and ideas", ImageUrl = "images/zen-temple.jpg" }
                        ,new Post{ Id = 3, DateCreated = DateTime.Now, Title = "Shaping up before work", ImageUrl = "images/meditation.png" }
                        ,new Post{ Id = 4, DateCreated = DateTime.Now, Title = "Construction begins!", ImageUrl = "images/coding.jpeg" }

                    };
                    foreach (var item in posts)
                    {
                        context.Posts.Add(item);
                    }
                    await context.SaveChangesAsync();
                }

                if (!context.Blocks.Any())
                {
                    var blocks = new List<Block>{
                        new Block{
                            Id = 1,
                            DateCreated = DateTime.Now,
                            Post = context.Posts.FirstOrDefault(p => p.Id == 1),
                            BlockType = context.BlockTypes.FirstOrDefault(b => b.Id == 1),
                            Content = "Test text block"
                        },
                        new Block{
                            Id = 2,
                            DateCreated = DateTime.Now,
                            Post = context.Posts.FirstOrDefault(p => p.Id == 1),
                            BlockType = context.BlockTypes.FirstOrDefault(b => b.Id == 2),
                            Content = "Test image block"
                        },
                         new Block{
                            Id = 3,
                            DateCreated = DateTime.Now,
                            Post = context.Posts.FirstOrDefault(p => p.Id == 2),
                            BlockType = context.BlockTypes.FirstOrDefault(b => b.Id == 1),
                            Content = "Right now concept of Zen Garden is to create your own temple where you can grow your inner strength. Main focus of this is to train self discipline through growing up trees inside temple's garden"
                        },
                         new Block{
                            Id = 4,
                            DateCreated = DateTime.Now,
                            Post = context.Posts.FirstOrDefault(p => p.Id == 3),
                            BlockType = context.BlockTypes.FirstOrDefault(b => b.Id == 1),
                            Content = "This site is easy in many steps, but our worker is very lazy. We need to shape it up, train him to do much more and open up his true potential. So shaping is required. Even easy sites is difficult at first, but everything is difficult at first"
                        },
                         new Block{
                            Id = 5,
                            DateCreated = DateTime.Now,
                            Post = context.Posts.FirstOrDefault(p => p.Id == 4),
                            BlockType = context.BlockTypes.FirstOrDefault(b => b.Id == 1),
                            Content = "The monumental building of simple angular site is begin! Start of construction is 27 march 2023. Site is very simple, so we don't know how many years it'l take to finish it, stay tuned!"
                        },
                    };
                    foreach (var item in blocks)
                    {
                        context.Blocks.Add(item);
                    }
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                var logger = factory.CreateLogger<SiteContextSeed>();
                logger.LogError(ex.Message);
            }
        }
    }
}