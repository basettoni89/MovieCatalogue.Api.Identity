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
        public void GetAll_FindAll()
        {
            var result = userRepository.BrowseUsers(new BrowseUser());

            Assert.Equal(2, result.Items.Count());
        }

        [Fact]
        public void GetByName_FindOne()
        {
            var result = userRepository.BrowseUsers(new BrowseUser() { Name = "Davide" });

            Assert.Single(result.Items);
            Assert.Collection(result.Items, x => Assert.Equal(1, x.ID));
        }

        [Fact]
        public void GetByName_FindNone()
        {
            var result = userRepository.BrowseUsers(new BrowseUser() { Name = "Pippo" });

            Assert.Empty(result.Items);
        }

        [Fact]
        public void GetByUsername_FindOne()
        {
            var result = userRepository.BrowseUsers(new BrowseUser() { Username = "erossi" });

            Assert.Single(result.Items);
            Assert.Collection(result.Items, x => Assert.Equal(2, x.ID));
        }

        [Fact]
        public void GetByUsername_FindNone()
        {
            var result = userRepository.BrowseUsers(new BrowseUser() { Username = "pluto" });

            Assert.Empty(result.Items);
        }

        [Fact]
        public void GetBySurname_FindOne()
        {
            var result = userRepository.BrowseUsers(new BrowseUser() { Surname = "Rossi" });

            Assert.Single(result.Items);
            Assert.Collection(result.Items, x => Assert.Equal(2, x.ID));
        }

        [Fact]
        public void GetBySurname_FindNone()
        {
            var result = userRepository.BrowseUsers(new BrowseUser() { Surname = "paperino" });

            Assert.Empty(result.Items);
        }
    }
}
