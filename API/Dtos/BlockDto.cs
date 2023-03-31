using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class BlockDto
    {
         public int Id { get; set; }
        public string? Content { get; set; }
        public int BlockTypeId { get; set; }
        public int PostId { get; set; }
    }
}