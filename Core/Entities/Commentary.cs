using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class Commentary : BaseEntity
    {
        public string? Message { get; set; }
        public string? AppUserId { get; set; }
        public int PostId { get; set; }
        [ForeignKey("AppUserId")]
        public virtual AppUser? AppUser { get; set; }
        [ForeignKey("PostId")]
        public virtual Post? Post { get; set; }
    }
}