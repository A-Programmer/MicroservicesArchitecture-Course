using AutoMapper;
using UsersService.Dtos;
using UsersService.Models;

namespace UsersService.Profiles
{
    public class PostsProfile : Profile
    {
        public PostsProfile()
        {
            // ==> Source => Target
            CreateMap<Post, ReadPostDto>();

            CreateMap<PostCreateDto, Post>();

            CreateMap<User, ReadUserDto>();
        }
    }
}