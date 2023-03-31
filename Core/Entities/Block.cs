using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class Block : BaseEntity
    {
        public string? Content { get; set; }
        public int PostId { get; set; }
        public int BlockTypeId { get; set; }
        [ForeignKey("PostId")]
        public Post? Post { get; set; }
        [ForeignKey("BlockTypeId")]
        public virtual BlockType? BlockType { get; set; }
    }
}