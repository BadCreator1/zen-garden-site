using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Commentary : BaseEntity
    {
        public string? Message { get; set; }

        public virtual AppUser? AppUser { get; set; }
        public virtual Post? Post { get; set; }
    }
}