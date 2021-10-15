using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UsersService.Data;
using UsersService.Dtos;
using UsersService.Models;

namespace UsersService.Controllers
{
    [ApiController]
    [Route("api/u/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersRepo _repo;
        private readonly IMapper _mapper;

        public UsersController(IUsersRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ReadUserDto>> GetAll()
        {
            Console.WriteLine(" ==> Getting Users List ...");
            var users = _repo.GetAllUsers();
            return Ok(_mapper.Map<IEnumerable<ReadUserDto>>(users));
        }

        [HttpGet("{id}", Name = "GetUserById")]
        public ActionResult<ReadUserDto> GetUserById(int id)
        {
            var user = _repo.GetUserById(id);
            return Ok(_mapper.Map<ReadUserDto>(user));
        }

        [HttpPost]
        public ActionResult<ReadUserDto> CreateUser(CreateUserDto userDto)
        {
            Console.WriteLine(" ==> Creating User ...");
            var user = _mapper.Map<User>(userDto);
            _repo.CreateUser(user);
            _repo.SaveChanges();
            var userReadDto = _mapper.Map<ReadUserDto>(user);
            return CreatedAtRoute(nameof(GetUserById), new { id = userReadDto.Id }, userReadDto);
        }
    }
}