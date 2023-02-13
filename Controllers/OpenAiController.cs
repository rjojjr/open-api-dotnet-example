using System;
using Microsoft.AspNetCore.Mvc;
using open_ai_example.Timer;
using open_ai_example.ai.Completions;
using System.Web;
using open_ai_example.Model;
using open_ai_example.ai.Completions.Transcripts;

namespace open_ai_example.Controllers
{

   

    [ApiController]
    [Route("open-ai/api/v1")]
    public class OpenAiController : BaseController
	{

        private readonly ILogger<OpenAiController> _logger;
        private readonly OpenAICompletionService _openAiCompletionService;
        private readonly OpenAIModelService _openAIModelService;
        private readonly OpenAIChatService _openAIChatService;
        private readonly OpenAIChatTranscriptService _openAIChatTranscriptService;

        public OpenAiController(ILogger<OpenAiController> logger, OpenAICompletionService openAiCompletionService, OpenAIModelService openAIModelService, OpenAIChatService openAIChatService, OpenAIChatTranscriptService openAIChatTranscriptService)
        {
            _logger = logger;
            _openAiCompletionService = openAiCompletionService;
            _openAIModelService = openAIModelService;
            _openAIChatService = openAIChatService;
            _openAIChatTranscriptService = openAIChatTranscriptService;
        }

        /// <summary>
        /// Processes given completion prompt with OpenAI.
        /// </summary>
        /// <remarks>Process completion for given prompt.</remarks>
        /// <response code="200">Success</response>
        /// <response code="400">Prompt is a required query parameter</response>
        /// <response code="500">Something went wrong</response>
        [HttpPost("completion")]
        public IActionResult GetCompletion([FromBody] CompletionRequest completionRequest, [FromQuery] string contextId = "")
        {
            var timer = Timer.Timer.TimerFactory(true);
            var resolvedContextId = ResolveContextId(contextId);

            _logger.LogInformation("received request to perform OpenAI text completion [contextId: {}]", resolvedContextId);
            return ExecuteWithExceptionHandler(() =>
            {
                var response = _openAiCompletionService.GetCompletion(HttpUtility.UrlDecode(completionRequest.Prompt), completionRequest.MaxTokens);

                _logger.LogInformation("completed successfully request to perform OpenAI text completion [contextId: {}, timeTaken: {}]", resolvedContextId, timer.GetTimeElasped());
                return Ok(response);
            });
        }

        /// <summary>
        /// Get OpenAI completion model with given name.
        /// </summary>
        /// <remarks>Get OpenAI completion model with given name.</remarks>
        /// <response code="200">Success</response>
        /// <response code="400">Prompt is a required query parameter</response>
        /// <response code="500">Something went wrong</response>
        [HttpGet("completion/model")]
        public IActionResult GetCompletionModel([FromQuery] string modelName = "", [FromQuery] string contextId = "")
        {
            var timer = Timer.Timer.TimerFactory(true);
            var resolvedContextId = ResolveContextId(contextId);

            _logger.LogInformation("received request to get OpenAI text completion model [contextId: {}, modelName: {}]", resolvedContextId, modelName);
            return ExecuteWithExceptionHandler(() =>
            {
                var response = _openAIModelService.GetModelByName(modelName);

                _logger.LogInformation("completed successfully request to get OpenAI text completion model [contextId: {}, modelName: {}, timeTaken: {}]", resolvedContextId, modelName, timer.GetTimeElasped());
                return Ok(response);
            });
        }

        /// <summary>
        /// Get OpenAI chat transcripts.
        /// </summary>
        /// <remarks>Get OpenAI chat transcripts.</remarks>
        /// <response code="200">Success</response>
        /// <response code="400">Prompt is a required query parameter</response>
        /// <response code="500">Something went wrong</response>
        [HttpGet("chat/transcripts")]
        public IActionResult GetChatTranscripts([FromQuery] string sessionId = "",[FromQuery] string contextId = "")
        {
            var timer = Timer.Timer.TimerFactory(true);
            var resolvedContextId = ResolveContextId(contextId);

            _logger.LogInformation("received request chat transcripts [contextId: {}, sessionId: {}]", resolvedContextId, sessionId);
            return ExecuteWithExceptionHandler(() =>
            {

                _logger.LogInformation("completed request chat transcripts[contextId: {}, sessionId: {}]", resolvedContextId, sessionId);
                return Ok(_openAIChatTranscriptService.GetChatTranscriptEntities(sessionId.Trim()));
            });
        }

        /// <summary>
        /// Send chat msg to model.
        /// </summary>
        /// <remarks>Send chat msg to model.</remarks>
        /// <response code="200">Success</response>
        /// <response code="400">Prompt is a required query parameter</response>
        /// <response code="500">Something went wrong</response>
        [HttpGet("chat")]
        public IActionResult Chat([FromQuery] string modelName = "", [FromQuery] string sessionId = "", [FromQuery] string message = "", [FromQuery] int maxTokens = 150, [FromQuery] string contextId = "")
        {
            var timer = Timer.Timer.TimerFactory(true);
            var resolvedContextId = ResolveContextId(contextId);
            var resolvedSessionId = ResolveSessionId(sessionId);

            _logger.LogInformation("received chat request [contextId: {}, sessionId: {}]", resolvedContextId, resolvedSessionId);
            return ExecuteWithExceptionHandler(() =>
            {

                _logger.LogInformation("completed chat request [contextId: {}, sessionId: {}]", resolvedContextId, resolvedSessionId);
                return Ok(_openAIChatService.ChatWithAIModel(modelName, "unknown", resolvedSessionId, maxTokens, message));
            });
        }

        /// <summary>
        /// Save OpenAI completion model.
        /// </summary>
        /// <remarks>Save OpenAI completion model.</remarks>
        /// <response code="201">Success</response>
        /// <response code="400">Prompt is a required query parameter</response>
        /// <response code="500">Something went wrong</response>
        [HttpPost("completion/model")]
        public IActionResult CreateModel([FromBody] CreateOpenAIModelRequest completionRequest,
            [FromQuery] string contextId = "",
            [FromQuery] bool urlEncoded = false)
        {
            var timer = Timer.Timer.TimerFactory(true);
            var resolvedContextId = ResolveContextId(contextId);

            _logger.LogInformation("received request to create OpenAI text completion model [contextId: {}, modelName: {}]", resolvedContextId, completionRequest.ModelName);
            return ExecuteWithExceptionHandler(() =>
            {
                var response = _openAIModelService.CreateCompletionModel(completionRequest.ModelName,
                    urlEncoded ? HttpUtility.UrlDecode(completionRequest.ModelRaw) : completionRequest.ModelRaw,
                    completionRequest.ModelAuthor,
                    completionRequest.ModelStop,
                    completionRequest.CostLevel,
                    completionRequest.Temperature,
                    completionRequest.ModelType);

                _logger.LogInformation("completed successfully request to create OpenAI text completion model [contextId: {}, modelName: {}, timeTaken: {}]", resolvedContextId, completionRequest.ModelName, timer.GetTimeElasped());
                return Created(".", response);
            });
        }

        /// <summary>
        /// Update existing OpenAI completion model.
        /// </summary>
        /// <remarks>Update existing OpenAI completion model.</remarks>
        /// <response code="200">Success</response>
        /// <response code="400">Prompt is a required query parameter</response>
        /// <response code="500">Something went wrong</response>
        [HttpPatch("completion/model")]
        public IActionResult UpdateModel([FromBody] CreateOpenAIModelRequest completionRequest,
            [FromQuery] string contextId = "",
            [FromQuery] bool urlEncoded = false)
        {
            var timer = Timer.Timer.TimerFactory(true);
            var resolvedContextId = ResolveContextId(contextId);

            _logger.LogInformation("received request to update OpenAI text completion model [contextId: {}, modelId: {}, modelName: {}]", resolvedContextId, completionRequest.ModelId, completionRequest.ModelName);
            return ExecuteWithExceptionHandler(() =>
            {
                var response = _openAIModelService.UpdateCompletionModel(completionRequest.ModelId,
                    completionRequest.ModelName,
                    urlEncoded ? HttpUtility.UrlDecode(completionRequest.ModelRaw) : completionRequest.ModelRaw,
                    completionRequest.ModelAuthor,
                    completionRequest.ModelStop,
                    completionRequest.CostLevel,
                    completionRequest.Temperature,
                    completionRequest.ModelType);

                _logger.LogInformation("completed successfully request to update OpenAI text completion model [contextId: {}, modelId: {}, modelName: {}, timeTaken: {}]", resolvedContextId, completionRequest.ModelId, completionRequest.ModelName, timer.GetTimeElasped());
                return Ok(response);
            });
        }

        private string ResolveContextId(string contextId)
        {
            if (contextId == null || contextId == "")
            {
                return Guid.NewGuid().ToString();
            }
            return contextId;
        }

        private string ResolveSessionId(string sessionId)
        {
            if(sessionId.Trim() == "")
            {
                return Guid.NewGuid().ToString();
            }
            return sessionId;
        }
    }
}

