using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data
{
    public class SiteContextSeed
    {
        public static async Task SeedAsync(SiteDbContext context,
        ILoggerFactory factory,
        UserManager<AppUser> userManager,
        RoleManager<IdentityRole> roleManager)
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

                string[] roleNames = new string[] { "Administrator", "Subscriber" };
                foreach (var roleName in roleNames)
                {
                    var roleExist = await roleManager.RoleExistsAsync(roleName);
                    if (!roleExist)
                    {
                        await roleManager.CreateAsync(new IdentityRole(roleName));
                    }
                }

                if (!context.Users.Any())
                {
                    var user = new AppUser
                    {
                        UserName = "Admin",
                        DisplayName = "Admin",
                        AvatarUrl = "avatar.png",
                        Email = "admin@test.com",
                        
                    };
                    await userManager.CreateAsync(user, "Pa$$w0rd");

                    //await context.SaveChangesAsync();
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