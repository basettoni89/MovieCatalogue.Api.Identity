using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using MovieCatalogue.Api.Identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCatalogue.Api.Identity.MongoDb
{
    public static class MongoDbExtensions
    {
        public static void AddMongoDb(this IServiceCollection services, Func<IMongoDbSettings> settingFunc)
        {
            IMongoDbSettings settings = settingFunc.Invoke();

            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            services.AddScoped<IMongoCollection<CounterModel>>(_ =>
            {
                return database.GetCollection<CounterModel>("counters");
            });

            services.AddScoped<IMongoCollection<UserModel>>(_ => 
            {
                return database.GetCollection<UserModel>(settings.CollectionName);
            });

            services.AddScoped<IQueryable<UserModel>>(_ => 
            {
                return database.GetCollection<UserModel>(settings.CollectionName).AsQueryable();
            });
        }

        public static IMongoDbSettings GetMongoDbSettings(this IConfiguration configuration, string name)
        {

            return configuration.GetSection(name).Get<MongoDbSettings>();
        }
    }
}
