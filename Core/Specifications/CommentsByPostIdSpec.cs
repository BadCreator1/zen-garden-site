using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Specifications
{
    public class CommentsByPostIdSpec : BaseSpecification<Commentary>
    {
        public CommentsByPostIdSpec(CommentSpecParams comParams)
        : base(x =>
                (comParams.PostId == x.Post.Id)
            )
        {
            AddInclude(x => x.AppUser);
            AddInclude(x => x.Post);
            ApplyPaging(comParams.PageSize * (comParams.PageIndex - 1)
                , comParams.PageSize);
        }

        public CommentsByPostIdSpec(int id) : base(x => x.Id == id)
        {
            AddInclude(x => x.AppUser);
            AddInclude(x => x.Post);
        }

    }
}