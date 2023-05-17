using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Core.Entities;

namespace API.Helpers
{
    public class PostDtoUrlResolver: IValueResolver<PostDto, Post, string>
    {
        private readonly IConfiguration _config;
        public PostDtoUrlResolver(IConfiguration config)
        {
            _config = config;
        }

        public string Resolve(PostDto source, Post destination, string destMember, ResolutionContext context)
        {
            if(!string.IsNullOrEmpty(source.ImageUrl)){
                return source.ImageUrl.Replace(_config["ApiUrl"],"");
            }
            return null;
        }
    }
}