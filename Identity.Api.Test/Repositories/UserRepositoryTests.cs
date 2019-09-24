using MovieCatalogie.Api.Identity.Queries;
using MovieCatalogue.Api.Identity.Models;
using MovieCatalogue.Api.Identity.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace MovieCatalogie.Api.Identity.Test.Repositories
{
    public class UserRepositoryTests
    {
        private static List<UserModel> testList = new List<UserModel>()
        {
            new UserModel(){ID = 1, Name = "Davide", Surname = "Mortara", Username = "charlie"},
            new UserModel(){ID = 2, Name = "Enrico", Surname = "Rossi", Username = "erossi"},
        };

        private readonly IUserRepository userRepository;

        public UserRepositoryTests()
        {
            userRepository = new UserRepository(testList.AsQueryable());
        }

        [Fact]
        public void GetAll()
        {
            var result = userRepository.BrowseUsers(new BrowseUser());

            Assert.Equal(2, result.Items.Count());
        }

        [Fact]
        public void GetByName()
        {
            var result = userRepository.BrowseUsers(new BrowseUser() { Name = "Davide" });

            Assert.Single(result.Items);
            Assert.Collection(result.Items, x => Assert.Equal("Davide", x.Name));
        }
    }
}
