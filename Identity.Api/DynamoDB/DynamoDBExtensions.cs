using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCatalogue.Api.Identity.DynamoDB
{
    public static class DynamoDBExtensions
    {
        public static void AddDynamoDb(this IServiceCollection serviceCollection, AmazonDynamoDBConfig config = null)
        {
            AmazonDynamoDBClient client;

            if(config == null)
            {
                client = new AmazonDynamoDBClient();
            }
            else
            {
                client = new AmazonDynamoDBClient(config);
            }

            var context = new DynamoDBContext(client);

            serviceCollection.AddSingleton<IDynamoDBContext>(context);
        }
    }
}
