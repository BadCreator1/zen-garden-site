using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using API.Helpers;
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
        public async Task<ActionResult<List<PostDto>>> GetPosts([FromQuery] NewsSpecParams newsParams)
        {

            var spec = new PostsWithBlocksSpec(newsParams);

            var countSpec = new PostsWithBlocksForCountSpec(newsParams);

            var totalItems = await _postsRepo.CountAsync(countSpec);

            var posts = await this._postsRepo.ListAsync(spec);

            var data = _mapper
            .Map<IReadOnlyList<Post>, IReadOnlyList<PostDto>>(posts);
            return Ok(new Pagination<PostDto>(newsParams.PageIndex,
             newsParams.PageSize, totalItems, data));
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

        [HttpGet("popular")]
        public async Task<ActionResult> GetPopularPosts()
        {
            var spec = new PopularPostsSpec();

            var posts = await this._postsRepo.ListAsync(spec);

            var data = _mapper
                       .Map<IReadOnlyList<Post>, IReadOnlyList<PostDto>>(posts);

            return Ok(data);

        }
        [HttpGet("latest")]
        public async Task<ActionResult> GetLatestPosts()
        {
            var spec = new LatestPostsSpec();

            var posts = await this._postsRepo.ListAsync(spec);

            var data = _mapper
                       .Map<IReadOnlyList<Post>, IReadOnlyList<PostDto>>(posts);

            return Ok(data);
        }
        [HttpPost]
        public async Task<ActionResult<PostDto>> UpdatePost(PostDto post)
        {
            var entityPost = new Post();
            if (post.Id > 0)
            {
                var spec = new PostsWithBlocksSpec(post.Id);
                entityPost = await this._postsRepo.GetEntityWithSpec(spec);
                _mapper.Map<PostDto, Post>(post, entityPost);
                await _postsRepo.UpdateEntity(entityPost);
            }
            else
            {
                _mapper.Map<PostDto, Post>(post, entityPost);
                entityPost = await _postsRepo.AddEntity(entityPost);
            }

            return _mapper.Map<Post, PostDto>(entityPost, post); ;
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePost(int Id)
        {
            var spec = new PostsWithBlocksSpec(Id);
            var entityPost = await this._postsRepo.GetEntityWithSpec(spec);
            await _postsRepo.DeleteEntity(entityPost);
            return Ok();
        }
    }
}