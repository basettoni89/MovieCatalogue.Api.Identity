using MovieCatalogue.Api.Identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCatalogue.Api.Identity.Repositories
{
    public interface IUserRepository
    {
        Task<UserModel> GetUserByID(int userId);
    }
}
