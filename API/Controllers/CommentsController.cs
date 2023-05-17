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
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentsController : ControllerBase
    {
        private IGenericRepository<Commentary> _commRepo;
        private IMapper _mapper;

        public CommentsController(IGenericRepository<Commentary> commRepo, IMapper mapper)
        {
            _commRepo = commRepo;
            _mapper = mapper;
        }

       
        [HttpPost]
        public async Task<ActionResult<CommentaryDto>> UpdateComment(CommentaryDto comment)
        {
            var entityPost = new Commentary();
            if (comment.Id > 0)
            {
                var spec = new CommentsByPostIdSpec(comment.Id);
                entityPost = await this._commRepo.GetEntityWithSpec(spec);
                _mapper.Map<CommentaryDto, Commentary>(comment, entityPost);
                await _commRepo.UpdateEntity(entityPost);
            }
            else
            {
                _mapper.Map<CommentaryDto, Commentary>(comment, entityPost);
                entityPost = await _commRepo.AddEntity(entityPost);
            }

            return  _mapper.Map<Commentary, CommentaryDto>(entityPost, comment);
        }


        [HttpGet]
        public async Task<ActionResult> GetComments([FromQuery] CommentSpecParams value)
        {
            var spec = new CommentsByPostIdSpec(value);
            var countSpec = new CommentsByPostIdForCountSpec(value);

            var comments = await _commRepo.ListAsync(spec);
            var totalItems = await _commRepo.CountAsync(countSpec);

            var data = _mapper
            .Map<IReadOnlyList<Commentary>, IReadOnlyList<CommentaryDto>>(comments);

            return Ok(new Pagination<CommentaryDto>(value.PageIndex,
             value.PageSize, totalItems, data));
        }
    }
}