using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;
using MovieCatalogue.Api.Identity.Models;

namespace MovieCatalogue.Api.Identity.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDynamoDBContext context;

        public UserRepository(IDynamoDBContext context)
        {
            this.context = context;
        }

        public async Task<UserModel> GetUserByID(int userId)
        {
            return await this.context.LoadAsync<UserModel>(userId);
        }
    }
}
