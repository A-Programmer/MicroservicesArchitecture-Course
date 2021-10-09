using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UsersService.Data;
using UsersService.Dtos;

namespace UsersService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
    }
}