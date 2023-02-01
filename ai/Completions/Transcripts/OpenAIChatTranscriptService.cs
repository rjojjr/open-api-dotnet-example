using System;
using open_ai_example.Exceptions;
using open_ai_example.Repository;

namespace open_ai_example.ai.Completions.Transcripts
{
	public class OpenAIChatTranscriptService
	{
		private readonly ILogger<OpenAIChatTranscriptService> _logger;
		private readonly ChatTranscriptRepository _chatTranscriptRepository;

        public OpenAIChatTranscriptService(ILogger<OpenAIChatTranscriptService> logger, ChatTranscriptRepository chatTranscriptRepository)
        {
            _logger = logger;
            _chatTranscriptRepository = chatTranscriptRepository;
        }

        public IList<ChatTranscriptEntity> GetChatTranscriptEntities(string sessionId = "")
        {
            return _chatTranscriptRepository.GetChatTranscriptEntities(sessionId);
        }

        public void CreateChatTranscript(ChatTranscriptEntry initialEntry, string modelId, int modelRevision, string sessionId)
        {
            _logger.LogDebug("creating transcript for chat session {}", sessionId);
            var timer = Timer.Timer.TimerFactory(true);
            var transcript = new ChatTranscriptEntity();
            transcript.Id = sessionId;
            transcript.ModelId = modelId;
            transcript.ModelRevision = modelRevision;
            transcript.StartedAt = initialEntry.SentAt;
            transcript.ChatTranscriptEntries.Add(initialEntry);

            _chatTranscriptRepository.CreateAsync(transcript).Wait();

            _logger.LogDebug("created transcript for chat session {}, took {} millis", sessionId, timer.GetTimeElasped());
        }

        public void UpdateChatTranscript(ChatTranscriptEntry entry, string sessionId)
        {
            _logger.LogDebug("updating transcript for chat session {}", sessionId);
            var timer = Timer.Timer.TimerFactory(true);
            var maybeTranscript = _chatTranscriptRepository.FindBySessionId(sessionId);

            if(maybeTranscript == null)
            {
                throw new HttpException($"transcript for chat session {sessionId} does not exist", System.Net.HttpStatusCode.BadRequest);
            }

            maybeTranscript.ChatTranscriptEntries.Add(entry);

            _chatTranscriptRepository.UpdateAsync(maybeTranscript).Wait();
            _logger.LogDebug("updated transcript for chat session {}, took {} millis", sessionId, timer.GetTimeElasped());
        }
    }
}

