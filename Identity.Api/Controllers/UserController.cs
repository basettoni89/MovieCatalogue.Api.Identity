﻿using Microsoft.AspNetCore.Mvc;
using MovieCatalogue.Api.Identity.Repositories;
using MovieCatalogue.Api.Identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieCatalogue.Api.Identity.Queries;
using MovieCatalogue.Api.Identity.Types;
using Microsoft.AspNetCore.Authorization;

namespace MovieCatalogue.Api.Identity.Controllers
{
    [Authorize]
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository;

        public UserController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        [HttpGet()]
        public PagedResult<UserModel> GetUsers([FromQuery] BrowseUser query)
        {
            return userRepository.BrowseUsers(query);
        }

        [HttpGet("{userId}")]
        public ActionResult<UserModel> GetUser(int userId)
        {
            var user = userRepository.GetUserByID(userId);

            if (user == null)
                return NotFound();

            return Ok(user);
        }
    }
}
