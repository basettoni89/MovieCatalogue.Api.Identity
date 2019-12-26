using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MovieCatalogue.Api.Identity.Jwt;
using MovieCatalogue.Api.Identity.Models;
using MovieCatalogue.Api.Identity.Repositories;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MovieCatalogue.Api.Identity.Controllers
{
    [Authorize]
    [Route("api")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository authRepository;
        private readonly IUserRepository userRepository;
        private readonly JwtSetting options;

        public AuthController(
            IAuthRepository authRepository,
            IUserRepository userRepository,
            IOptions<JwtSetting> options)
        {
            this.authRepository = authRepository;
            this.userRepository = userRepository;
            this.options = options.Value;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<UserModel>> RegisterUser([FromBody] RegisterModel data)
        {
            UserModel user = await authRepository.Register(data.Username, data.Name, data.Surname);

            if (user == null)
                return BadRequest();

            return user;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public ActionResult<string> Login([FromBody] LoginModel data)
        {
            UserModel user = userRepository.GetUserByUsername(data.Username);

            if (user == null)
                return Unauthorized();

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(options.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.ID.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return Ok(tokenHandler.WriteToken(token));
        }
    }
}
