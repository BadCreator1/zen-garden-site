using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Post : BaseEntity
    {
        public string? Title { get; set; }
        public string? ImageUrl { get; set; }
        public AppUser? AppUser { get; set; }
        public List<Block>? Blocks { get; set; }
        public List<Commentary>? Commentaries { get; set; }
        public int Views { get; set; }

    }
}