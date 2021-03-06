
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PostsService.AsyncDataServices;
using PostsService.Data;
using PostsService.Dtos;
using PostsService.Models;
using PostsService.SyncDataServices;

namespace PostsService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly IPostRepository _repo;
        private readonly IMapper _mapper;
        private readonly IMessageBusClient _messageBusClient;
        private readonly IUserDataClient _client;

        public PostsController(IPostRepository repo,
            IMapper mapper,
            IMessageBusClient messageBusClient,
            IUserDataClient client)
        {
            _repo = repo;
            _mapper = mapper;
            _messageBusClient = messageBusClient;
            _client = client;
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
        public async Task<ActionResult<PostReadDto>> CreatePost(PostCreateDto postCreateDto)
        {
            var post = _mapper.Map<Post>(postCreateDto);
            _repo.CreatePost(post);
            _repo.SaveChanges();

            var postReadDto = _mapper.Map<PostReadDto>(post);

            //Sync Messaging
            try
            {
                await _client.SendPostToUsers(postReadDto);
            }
            catch(Exception ex)
            {
                Console.WriteLine(" ==> Could not send synchronously: " + ex.Message);
            }

            //Async Messaging
            try
            {
                var postPublishedDto = _mapper.Map<PostPublishedDto>(postReadDto);
                postPublishedDto.Event = "Post_Published";
                _messageBusClient.PublishNewPost(postPublishedDto);
            }
            catch(Exception ex)
            {
                Console.WriteLine($" ==> Could not send message, {ex.Message}");
            }

            return CreatedAtRoute(nameof(GetPostById), new { id = postReadDto.Id }, postReadDto);
        }
    }
}