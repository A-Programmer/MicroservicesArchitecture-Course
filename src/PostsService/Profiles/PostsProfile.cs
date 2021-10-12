using AutoMapper;
using PostsService.Dtos;
using PostsService.Models;

namespace PostsService.Profiles
{
    public class PostsProfile : Profile
    {
        public PostsProfile()
        {
            // Source => Destination
            CreateMap<Post, PostReadDto>();
            CreateMap<PostCreateDto, Post>();
            CreateMap<PostReadDto, PostPublishedDto>();
        }
    }
}