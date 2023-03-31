using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class SiteDbContext : IdentityDbContext
    {
        public SiteDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Block> Blocks { get; set; }
        public DbSet<BlockType> BlockTypes { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Commentary> Commentaries { get; set; }
        public DbSet<Post> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}