using System;
using open_ai_example.ai.Completions.Transcripts;
using open_ai_example.Controllers;
using open_ai_example.Model;
using open_ai_example.Repository;

namespace open_ai_example.ai.Completions
{
	public class OpenAIChatService
	{

        private readonly ILogger<OpenAIChatService> _logger;
        private readonly OpenAICompletionService _openAiCompletionService;
        private readonly OpenAIModelService _openAIModelService;
        private readonly OpenAIChatTranscriptService _openAIChatTranscriptService;

        public OpenAIChatService(ILogger<OpenAIChatService> logger,
            OpenAICompletionService openAiCompletionService,
            OpenAIModelService openAIModelService,
            OpenAIChatTranscriptService openAIChatTranscriptService)
        {
            _logger = logger;
            _openAiCompletionService = openAiCompletionService;
            _openAIModelService = openAIModelService;
            _openAIChatTranscriptService = openAIChatTranscriptService;
        }

        public TextCompletionResponse ChatWithAIModel(string modelName,
            string userName,
            string sessionId,
            int maxTokens,
            string msg,
            bool includePreviousContext = false)
        {

            var model = _openAIModelService.GetModelByName(modelName);
            // TODO - Check for session
            var transcripts = _openAIChatTranscriptService.GetChatTranscriptEntities(sessionId).Transcripts;
            var transcript = transcripts.Count() > 0 ? transcripts[0] : null;

            var userEntry = new ChatTranscriptEntry();
            userEntry.ParticipantType = ChatParticipantType.HUMAN;
            // TODO - Get user id
            userEntry.ParticipantName = ChatParticipantType.HUMAN.ToString();

            userEntry.SentAt = DateTime.UtcNow;
            userEntry.Message = msg;

            if (transcript == null)
            {
                _openAIChatTranscriptService.CreateChatTranscript(userEntry, model.Id, model.CurrentRevision, sessionId);
            } else
            {
                _openAIChatTranscriptService.UpdateChatTranscript(userEntry, sessionId);
            }

            var prompt = model.ModelType == ModelType.CHAT_PROMPT
                ? model.ModelRaw + "\nHuman: " + msg + "\nAI:"
                : "Human: " + msg + "\nAI:";

            var aiEntry = new ChatTranscriptEntry();
            aiEntry.ParticipantType = ChatParticipantType.AI;
            // TODO - Get user id
            aiEntry.ParticipantName = ChatParticipantType.AI.ToString();

            aiEntry.SentAt = DateTime.UtcNow;

            var response = _openAiCompletionService.GetCompletion(prompt, maxTokens, model.CurrentCostLevel, model.Temperature, model.ModelType, model.ModelType == ModelType.CHAT_PROMPT ? "" : model.ModelRaw);
            aiEntry.Message = response.Response;
            _openAIChatTranscriptService.UpdateChatTranscript(aiEntry, sessionId);

            return response;
        }
    }
}

