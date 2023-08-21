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
                        // new Post{ 
                        //     Id = 1, 
                        //     DateCreated = DateTime.Now, 
                        //     Title = "Fuji San", 
                        //     ImageUrl = "https://interacnetwork.com/the-content/cream/wp-content/uploads/2021/11/pexels-toma%CC%81s%CC%8C-mali%CC%81k-3408354-1.jpeg",
                        //     jsonDoc = "<h2 style=\\\"text-align:center\\\"><strong>Fuji San</strong></h2><p></p><p style=\\\"text-align:center\\\"><img src=\\\"https://interacnetwork.com/the-content/cream/wp-content/uploads/2021/11/pexels-toma%CC%81s%CC%8C-mali%CC%81k-3408354-1.jpeg\\\" alt=\\\"\\\" title=\\\"Fuji San\\\" width=\\\"861px\\\"></p><p style=\\\"text-align:center\\\"></p><h3><strong><span style=\\\"color:rgb(0, 0, 0);\\\"><span style=\\\"background-color:rgb(255, 255, 255);\\\">Section 1.10.32 of \\\"de Finibus Bonorum et Malorum\\\", written by Cicero in 45 BC</span></span></strong></h3><p style=\\\"text-align:justify\\\"><span style=\\\"color:rgb(0, 0, 0);\\\"><span style=\\\"background-color:rgb(255, 255, 255);\\\">Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo. Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt. Neque porro quisquam est, qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit, sed quia non numquam eius modi tempora incidunt ut labore et dolore magnam aliquam quaerat voluptatem. Ut enim ad minima veniam, quis nostrum exercitationem ullam corporis suscipit laboriosam, nisi ut aliquid ex ea commodi consequatur? Quis autem vel eum iure reprehenderit qui in ea voluptate velit esse quam nihil molestiae consequatur, vel illum qui dolorem eum fugiat quo voluptas nulla pariatur?\\\"</span></span></p><p></p>"
                        //     }
                        // ,new Post{ 
                        //     Id = 2, 
                        //     DateCreated = DateTime.Now, 
                        //     Title = "Finding concepts and ideas", 
                        //     ImageUrl ="https://localhost:7036/images/347564db-7fe7-496c-b021-842bf483a664.png",
                        //     jsonDoc="<h1>Finding concepts and ideas</h1><p></p><p style=\\\"text-align:center\\\"><img src=\\\"https://localhost:7036/images\\\\347564db-7fe7-496c-b021-842bf483a664.png\\\"></p><p></p><p>Right now concept of Zen Garden is to create your own temple where you can grow your inner strength. Main focus of this is to train self discipline through growing up trees inside temple\'s garden</p><p></p>"
                        //     }
                        // ,new Post{ 
                        //     Id = 3, 
                        //     DateCreated = DateTime.Now, 
                        //     Title = "Shaping up before work", 
                        //     ImageUrl = "https://localhost:7036/images/meditation.png",
                        //     jsonDoc="<h1>Shaping up before work</h1><p><img src=\"https://localhost:7036/images\\meditation.png\" width=\"807px\"></p><p style=\"text-align:center\"></p><p>This site is easy in many steps, but our worker is very lazy. We need to shape it up, train him to do much more and open up his true potential. So shaping is required. Even easy sites is difficult at first, but everything is difficult at first</p><p></p>"
                        //     }
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
                   await userManager.AddToRoleAsync(user, "Administrator");
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