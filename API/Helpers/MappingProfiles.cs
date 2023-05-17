using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Core.Entities;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Block, BlockDto>()
            //.ForMember(d => d.BlockTypeId, o => o.MapFrom(s => s.BlockType.Id))
             .ForMember(d => d.PostId, o => o.MapFrom(s => s.Post.Id));

            CreateMap<Commentary, CommentaryDto>()
            .ForMember(d => d.AppUserId, o => o.MapFrom(s => s.AppUser.Id))
            .ForMember(d => d.PostId, o => o.MapFrom(s => s.Post.Id))
            .ForMember(d => d.UserName, o => o.MapFrom(s => s.AppUser.DisplayName))
            .ForMember(d => d.AvatarUrl, o => o.MapFrom(s => s.AppUser.AvatarUrl));

            CreateMap<CommentaryDto, Commentary>();
            

            CreateMap<Post, PostDto>()
             .ForMember(d => d.ImageUrl, o => o.MapFrom<PostUrlResolver>())
            .ForMember(d => d.UserId, o => o.MapFrom(s => s.AppUser.Id));

            CreateMap<PostDto, Post>()
             .ForMember(d => d.ImageUrl, o => o.MapFrom<PostDtoUrlResolver>());

            CreateMap<BlockDto, Block>();

        }
    }
}