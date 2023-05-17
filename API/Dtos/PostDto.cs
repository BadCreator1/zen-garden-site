using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class PostDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? ImageUrl { get; set; }
        public string? UserId { get; set; }
        public List<BlockDto>? Blocks { get; set; }
        public List<CommentaryDto>? Commentaries { get; set; }
        public int Views { get; set; }
        public string? jsonDoc { get; set; }
        public string? Description { get; set; }
    }
}