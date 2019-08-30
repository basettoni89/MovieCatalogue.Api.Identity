using Microsoft.AspNetCore.Mvc;
using Moq;
using MovieCatalogue.Api.Identity.Controllers;
using MovieCatalogue.Api.Identity.Models;
using MovieCatalogue.Api.Identity.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MovieCatalogue.Api.Identity.Test.Controllers
{
    public class UserControllerTests
    {
        private readonly UserController userController;

        public UserControllerTests()
        {
            userController = GetController();
        }

        private UserController GetController()
        {
            var mockRepository = new Mock<IUserRepository>();
            mockRepository.Setup(x => x.GetUserByID(It.IsAny<int>()))
                .Returns((int ID) => ID > 0 && ID < 10 ?
                    Task.FromResult(new UserModel { ID = ID }) : Task.FromResult<UserModel>(null));

            return new UserController(mockRepository.Object);
        }

        [Fact]
        public async void GetUserById_ReturnOkResult()
        {
            var result = await userController.GetUser(1);

            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async void GetUserById_ReturnUser()
        {
            var result = (await userController.GetUser(1)).Result as OkObjectResult;

            Assert.IsType<UserModel>(result.Value);
        }

        [Fact]
        public async void GetUserById_ReturnNotFound()
        {
            var result = await userController.GetUser(100);

            Assert.IsType<NotFoundResult>(result.Result);
        }
    }
}
