using MovieCatalogue.Api.Identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCatalogue.Api.Identity.Repositories
{
    public interface IAuthRepository
    {
        Task<UserModel> Register(string username, string name, string surname);
    }
}
