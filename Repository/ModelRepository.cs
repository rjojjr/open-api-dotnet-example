using System;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using open_ai_example.ai.Completions;
using open_ai_example.Config;

namespace open_ai_example.Repository
{
	public class ModelRepository
	{

        private readonly IMongoCollection<OpenAICompletionModel> _modelCollection;

        public ModelRepository(
            IOptions<ModelDbConfig> databaseConfig)
        {

            MongoCredential credential = MongoCredential.CreateCredential(
                "admin",
                databaseConfig.Value.Username,
                databaseConfig.Value.Password
            );
            var settings = new MongoClientSettings
            {
                Credential = credential,
                Server = new MongoServerAddress(
                    databaseConfig.Value.Host,
                    databaseConfig.Value.Port
                ),
                SocketTimeout = new TimeSpan(0, 3, 0),
                WaitQueueTimeout = new TimeSpan(0, 3, 0),
                ConnectTimeout = new TimeSpan(0, 3, 0)
            };
            var mongoClient = new MongoClient(settings);

            var mongoDatabase = mongoClient.GetDatabase(
                databaseConfig.Value.DatabaseName);

            _modelCollection = mongoDatabase.GetCollection<OpenAICompletionModel>(
                "open_ai_models");
        }

        public async Task CreateAsync(OpenAICompletionModel newEvent) =>
           await _modelCollection.InsertOneAsync(newEvent);

        public OpenAICompletionModel FindByModelName(string modelName)
        {
            var results = _modelCollection.Find(x => x.ModelName == modelName);
            if(results.Count() > 0)
            {
                return results.First();
            }
            return null;
        }

    }
}

