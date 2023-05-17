using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Specifications
{
    public class PostsWithBlocksForCountSpec : BaseSpecification<Post>
    {
        public PostsWithBlocksForCountSpec(NewsSpecParams newsParams) : base(x =>
                (string.IsNullOrEmpty(newsParams.Search) || x.Title.ToLower().Contains(newsParams.Search.ToLower()))
            )
        {
        }
    }
}