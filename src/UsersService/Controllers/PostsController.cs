using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UsersService.Data;
using UsersService.Dtos;
using UsersService.Models;

namespace UsersService.Controllers
{
    [Route("api/users/{userid}/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IUsersRepo _repo;
        private readonly IMapper _mapper;

        public PostsController(IUsersRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        //Get user posts
        //Get Post
        //Create Post

        [HttpGet]
        public ActionResult<IEnumerable<ReadPostDto>> GetUserPosts(int userId)
        {
            Console.WriteLine($" ==> Getting Posts for User Id: {userId}");

            if(!_repo.IsUserExist(userId))
            {
                return NotFound();
            }

            var posts = _repo.GetUserPosts(userId);

            return Ok(_mapper.Map<IEnumerable<ReadPostDto>>(posts));
        }

        [HttpGet("{postId}", Name = "GetPostForUser")]
        public ActionResult<ReadPostDto> GetPostForUser(int userId, int postId)
        {
            Console.WriteLine($"Getting one post for user with Id: {userId}, and Post ID: {postId}");
            var post = _repo.GetPost(userId, postId);
            if(post == null)
                return NotFound();
            
            return Ok(_mapper.Map<ReadPostDto>(post));
        }

        [HttpPost]
        public ActionResult<ReadPostDto> CreatePostForUser(int userId, PostCreateDto postDto)
        {
            Console.WriteLine($" ==> Hit CreatePostForUser with UserID: {userId}");

            if(!_repo.IsUserExist(userId))
                return NotFound();

            if(postDto == null)
                return NotFound();
            var post = _mapper.Map<Post>(postDto);
            _repo.CreatePost(userId, post);
            _repo.SaveChanges();

            var postReadDto = _mapper.Map<ReadPostDto>(post);

            return CreatedAtRoute(nameof(GetPostForUser),
                new { userId = userId, postId = postReadDto.Id}, postReadDto);
        }

        // [HttpPost]
        // public IActionResult TestInboundConnection()
        // {
        //     Console.WriteLine("Inbound POST # Users Service");

        //     return Ok("Inbound test from Posts Controller");
        // }
    }
}