using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class CommentaryDto
    {
        public int Id { get; set; }
        public string? Message { get; set; }
        public string? AppUserId { get; set; }
        public int PostId { get; set; }
        public string? UserName { get; set; }
        public string? AvatarUrl { get; set; }
    }
}