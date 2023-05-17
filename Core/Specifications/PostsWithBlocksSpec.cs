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
        public PostsWithBlocksSpec(NewsSpecParams newsParams)
        : base(x => 
                (string.IsNullOrEmpty(newsParams.Search) || x.Title.ToLower().Contains(newsParams.Search.ToLower()))            
            )
        {
            AddInclude(x => x.Blocks);
            ApplyPaging(newsParams.PageSize * (newsParams.PageIndex - 1)
                , newsParams.PageSize);
        }
        public PostsWithBlocksSpec(int id) : base(x => x.Id == id)
        {
            AddInclude(x => x.Blocks);
        }
    }
}