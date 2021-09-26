using System;
using Microsoft.AspNetCore.Mvc;

namespace UsersService.Controllers
{
    [Route("api/u/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        public PostsController()
        {
            
        }

        [HttpPost]
        public IActionResult TestInboundConnection()
        {
            Console.WriteLine("Inbound POST # Users Service");

            return Ok("Inbound test from Posts Controller");
        }
    }
}