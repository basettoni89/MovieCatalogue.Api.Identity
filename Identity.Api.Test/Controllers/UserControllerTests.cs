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
                    new UserModel { ID = ID } : null);

            return new UserController(mockRepository.Object);
        }

        [Fact]
        public void GetUserById_ReturnOkResult()
        {
            var result = userController.GetUser(1);

            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GetUserById_ReturnUser()
        {
            var result = userController.GetUser(1).Result as OkObjectResult;

            Assert.IsType<UserModel>(result.Value);
        }

        [Fact]
        public void GetUserById_ReturnNotFound()
        {
            var result = userController.GetUser(100);

            Assert.IsType<NotFoundResult>(result.Result);
        }
    }
}
