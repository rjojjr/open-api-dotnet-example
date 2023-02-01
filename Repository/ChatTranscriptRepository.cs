using System;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using open_ai_example.ai.Completions;
using open_ai_example.ai.Completions.Transcripts;
using open_ai_example.Config;

namespace open_ai_example.Repository
{
	public class ChatTranscriptRepository
	{
        private readonly IMongoCollection<ChatTranscriptEntity> _transcriptCollection;

        public ChatTranscriptRepository(
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

            _transcriptCollection = mongoDatabase.GetCollection<ChatTranscriptEntity>(
                "open_ai_chat_transcripts");
        }

        public IList<ChatTranscriptEntity> GetChatTranscriptEntities(string sessionId = "")
        {
            return _transcriptCollection.Find(x => sessionId == "" ? true : x.SessionId == sessionId).ToList();
        }

        public async Task UpdateAsync(ChatTranscriptEntity updatedTranscript) =>
           await _transcriptCollection.ReplaceOneAsync(x => x.SessionId == updatedTranscript.SessionId, updatedTranscript);

        public async Task CreateAsync(ChatTranscriptEntity newTranscript) =>
           await _transcriptCollection.InsertOneAsync(newTranscript);

        public ChatTranscriptEntity FindBySessionId(string modelName)
        {
            var results = _transcriptCollection.Find(x => x.SessionId == modelName).ToList();
            if (results.Count() > 0)
            {
                return results[0];
            }
            return null;
        }
    }
}

