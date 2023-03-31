using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly IGenericRepository<Post> _postsRepo;
        private readonly IMapper _mapper;
        private readonly SiteDbContext _context;
        public PostsController(IGenericRepository<Post> postsRepo, IMapper mapper, SiteDbContext context)
        {
            _context = context;
            _mapper = mapper;
            _postsRepo = postsRepo;
        }

        [HttpGet]
        public async Task<ActionResult<List<PostDto>>> GetPosts()
        {
            var spec = new PostsWithBlocksSpec();
            var posts = await this._postsRepo.ListAsync(spec);
            //var posts = _context.Posts.Include(b => b.Blocks.Select(p => p.BlockType)).ToList();
            var data = _mapper
            .Map<IReadOnlyList<Post>, IReadOnlyList<PostDto>>(posts);
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PostDto>> GetPost(int id)
        {
            var spec = new PostsWithBlocksSpec(id);
            var post = await this._postsRepo.GetEntityWithSpec(spec);
            var data = _mapper
           .Map<Post, PostDto>(post);
            return Ok(data);
        }
        [HttpPost]
        public async Task<ActionResult> UpdatePost(PostDto post){
            var spec = new PostsWithBlocksSpec(post.Id);
            var entityPost = await this._postsRepo.GetEntityWithSpec(spec);
            
            _mapper.Map<PostDto, Post>(post, entityPost);

            await _postsRepo.UpdateEntity(entityPost);
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePost(int Id){
            var spec = new PostsWithBlocksSpec(Id);
            var entityPost = await this._postsRepo.GetEntityWithSpec(spec);
            await _postsRepo.DeleteEntity(entityPost);
            return Ok();
        }
    }
}