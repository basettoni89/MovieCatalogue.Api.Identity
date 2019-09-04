using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using MovieCatalogue.Api.Identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Api.MongoDb
{
    public static class MongoDbExtensions
    {
        public static void AddMongoDb(this IServiceCollection services, Func<IMongoDbSettings> settingFunc)
        {
            services.AddScoped<IMongoCollection<UserModel>>(_ => {
                IMongoDbSettings settings = settingFunc.Invoke();

                var client = new MongoClient(settings.ConnectionString);
                var database = client.GetDatabase(settings.DatabaseName);

                return database.GetCollection<UserModel>(settings.CollectionName);
            });
        }

        public static IMongoDbSettings GetMongoDbSettings(this IConfiguration configuration, string name)
        {

            return configuration.GetSection(name).Get<MongoDbSettings>();
        }
    }
}
