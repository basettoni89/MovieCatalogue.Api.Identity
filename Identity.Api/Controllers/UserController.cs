using Microsoft.AspNetCore.Mvc;
using MovieCatalogue.Api.Identity.Repositories;
using MovieCatalogue.Api.Identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCatalogue.Api.Identity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository;

        public UserController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<UserModel>> GetUser(int userId)
        {
            var user = await userRepository.GetUserByID(userId);

            if (user == null)
                return NotFound();

            return Ok(user);
        }
    }
}
