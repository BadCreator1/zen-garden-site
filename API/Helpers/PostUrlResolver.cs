using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Core.Entities;

namespace API.Helpers
{
    public class PostUrlResolver: IValueResolver<Post, PostDto, string>
    {
        private readonly IConfiguration _config;
        public PostUrlResolver(IConfiguration config)
        {
            _config = config;
        }

        public string Resolve(Post source, PostDto destination, string destMember, ResolutionContext context)
        {
            if(!string.IsNullOrEmpty(source.ImageUrl) && !source.ImageUrl.Contains("http")){
                return _config["ApiUrl"] + source.ImageUrl;
            }
            return source.ImageUrl;
        }
      
    }
}