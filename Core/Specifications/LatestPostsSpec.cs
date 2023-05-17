using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Specifications
{
    public class LatestPostsSpec: BaseSpecification<Post>
    {
        public LatestPostsSpec(){
            AddOrderBy(p => p.Views);
            ApplyPaging(0, 10);
        }
    }
}