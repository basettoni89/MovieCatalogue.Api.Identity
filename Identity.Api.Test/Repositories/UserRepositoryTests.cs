using Amazon.DynamoDBv2.DataModel;
using Moq;
using MovieCatalogue.Api.Identity.Models;
using MovieCatalogue.Api.Identity.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace MovieCatalogue.Api.Identity.Test.Repositories
{
    public class UserRepositoryTests
    {
        private readonly UserRepository userRepo;

        public UserRepositoryTests()
        {
            userRepo = GetRepository();
        }

        private UserRepository GetRepository()
        {
            var mockContext = new Mock<IDynamoDBContext>();
            mockContext.Setup(x => x.LoadAsync<UserModel>(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .Returns((int ID, CancellationToken ct) => ID > 0 && ID < 10 ? 
                    Task.FromResult(new UserModel { ID = ID}) : Task.FromResult<UserModel>(null));

            var repo = new UserRepository(mockContext.Object);

            return repo;
        }

        [Fact]
        public async void GetUserById_UserFound()
        {
            UserModel user = await userRepo.GetUserByID(1);

            Assert.NotNull(user);
        }

        [Fact]
        public async void GetUserById_UserNotFound()
        {
            UserModel user = await userRepo.GetUserByID(100);

            Assert.Null(user);
        }
    }
}
