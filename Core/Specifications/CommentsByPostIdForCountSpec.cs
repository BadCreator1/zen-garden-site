using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Specifications
{
    public class CommentsByPostIdForCountSpec: BaseSpecification<Commentary>
    {
        public CommentsByPostIdForCountSpec(CommentSpecParams comParams)
        : base(x => 
                (comParams.PostId == x.Post.Id)            
            )
        {
            AddInclude(x => x.AppUser);
        }
    }
}