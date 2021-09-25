
using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PostsService.Data;
using PostsService.Dtos;
using PostsService.Models;

namespace PostsService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly IPostRepository _repo;
        private readonly IMapper _mapper;

        public PostsController(IPostRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PostReadDto>> GetAllPosts()
        {
            Console.WriteLine(" ==> Getting all posts ...");

            var posts = _repo.GetAllPosts();
            var postReadItems = _mapper.Map<IEnumerable<PostReadDto>>(posts);

            return Ok(postReadItems);
        }

        [HttpGet("{id}", Name = "GetPostById")]
        public ActionResult<PostReadDto> GetPostById(int id)
        {
            Console.WriteLine($" ==> Getting post by {id} id.");
            var post = _repo.GetPostById(id);
            if(post == null)
                return NotFound();
            
            return Ok(_mapper.Map<PostReadDto>(post));
        }

        [HttpPost]
        public ActionResult<PostReadDto> CreatePost(PostCreateDto postCreateDto)
        {
            var post = _mapper.Map<Post>(postCreateDto);
            _repo.CreatePost(post);
            _repo.SaveChanges();

            var postReadDto = _mapper.Map<PostReadDto>(post);

            return CreatedAtRoute(nameof(GetPostById), new { id = postReadDto.Id }, postReadDto);
        }
    }
}