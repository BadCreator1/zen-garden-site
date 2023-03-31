using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Specifications
{
    public class PostsWithBlocksSpec : BaseSpecification<Post>
    {
        public PostsWithBlocksSpec()
        {
            AddInclude(x => x.Blocks);
        }
        public PostsWithBlocksSpec(int id) : base(x => x.Id == id)
        {
            AddInclude(x => x.Blocks);;
        }
    }
}