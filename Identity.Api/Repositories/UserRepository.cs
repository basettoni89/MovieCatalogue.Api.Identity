using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using MovieCatalogue.Api.Identity.Models;

namespace MovieCatalogue.Api.Identity.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<UserModel> context;

        public UserRepository(IMongoCollection<UserModel> context)
        {
            this.context = context;
        }

        public async Task<UserModel> GetUserByID(int userId)
        {
            return await this.context.Find(x => x.ID == userId).FirstOrDefaultAsync();
        }
    }
}
