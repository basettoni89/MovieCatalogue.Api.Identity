using Microsoft.AspNetCore.Mvc;
using MovieCatalogue.Api.Identity.Models;
using MovieCatalogue.Api.Identity.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCatalogue.Api.Identity.Controllers
{
    [Route("api")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository repository;

        public AuthController(IAuthRepository repository)
        {
            this.repository = repository;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserModel>> RegisterUser([FromBody] RegisterModel data)
        {
            UserModel user = await repository.Register(data.Username, data.Name, data.Surname);

            if (user == null)
                return BadRequest();

            return user;
        }
    }
}
