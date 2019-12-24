using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using MovieCatalogue.Api.Identity.Models;

namespace MovieCatalogue.Api.Identity.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IMongoCollection<UserModel> users;
        private readonly IMongoCollection<CounterModel> counters;

        public AuthRepository(IMongoCollection<UserModel> users, IMongoCollection<CounterModel> counters)
        {
            this.users = users;
            this.counters = counters;
        }

        public async Task<UserModel> Register(string username, string name, string surname)
        {
            if(users.AsQueryable().Where(x => x.Username == username).Any())
            {
                return null;
            }

            UserModel user = new UserModel()
            {
                ID = GetNextId(),
                Username = username,
                Name = name,
                Surname = surname
            };

            await users.InsertOneAsync(user);

            return user;
        }

        private int GetNextId()
        {
            var filter = Builders<CounterModel>.Filter.Eq("Id", "userId");
            var update = Builders<CounterModel>.Update.Inc(s => s.Value, 1);

            var options = new FindOneAndUpdateOptions<CounterModel> { IsUpsert = true, ReturnDocument = ReturnDocument.After };
            CounterModel c = counters.FindOneAndUpdate(filter, update, options);
            
            return c.Value;
        }
    }
}
