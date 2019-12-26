using MovieCatalogue.Api.Identity.Queries;
using MovieCatalogue.Api.Identity.Types;
using MovieCatalogue.Api.Identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCatalogue.Api.Identity.Repositories
{
    public interface IUserRepository
    {
        PagedResult<UserModel> BrowseUsers(BrowseUser query);

        UserModel GetUserByID(int userId);

        UserModel GetUserByUsername(string username);
    }
}
