using System;
using Microsoft.AspNetCore.Mvc;
using open_ai_example.Timer;
using open_ai_example.ai.Completions;
using System.Web;
using open_ai_example.Model;

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

        public OpenAiController(ILogger<OpenAiController> logger,
            OpenAICompletionService openAiCompletionService,
            OpenAIModelService openAIModelService,
            OpenAIChatService openAIChatService)
        {
            _logger = logger;
            _openAiCompletionService = openAiCompletionService;
            _openAIModelService = openAIModelService;
            _openAIChatService = openAIChatService;
        }

        /// <summary>
        /// Processes given completion prompt.
        /// </summary>
        /// <remarks>Process completion for given prompt.</remarks>
        /// <response code="200">Success</response>
        /// <response code="400">Prompt is a required query parameter</response>
        /// <response code="500">Something went wrong</response>
        [HttpPost("completion")]
        public IActionResult GetCompletion([FromBody] CompletionRequest completionRequest, [FromQuery] string contextId = "")
        {
            var timer = Timer.Timer.TimerFactory(true);
            var resolvedContextId = "";

            if(contextId == null || contextId == "")
            {
                resolvedContextId = Guid.NewGuid().ToString();
            }
            else
            {
                resolvedContextId = contextId;
            }
            _logger.LogInformation("received request to perform OpenAI text completion [contextId: {}]", resolvedContextId);
            return ExecuteWithExceptionHandler(() =>
            {
                var response = _openAiCompletionService.GetCompletion(HttpUtility.UrlDecode(completionRequest.Prompt), completionRequest.MaxTokens);

                _logger.LogInformation("completed successfully request to perform OpenAI text completion [contextId: {}, timeTaken: {}]", resolvedContextId, timer.GetTimeElasped());
                return Ok(response);
            });
        }

        /// <summary>
        /// Chat with OpenAI model.
        /// </summary>
        /// <remarks>Chat with OpenAI model.</remarks>
        /// <response code="200">Success</response>
        /// <response code="400">Prompt is a required query parameter</response>
        /// <response code="500">Something went wrong</response>
        [HttpGet("chat")]
        public IActionResult GetCompletion([FromQuery] string modelName, [FromQuery] string sessionId, [FromQuery] string message, [FromQuery] string contextId = "")
        {
            var timer = Timer.Timer.TimerFactory(true);
            var resolvedContextId = "";

            if (contextId == null || contextId == "")
            {
                resolvedContextId = Guid.NewGuid().ToString();
            }
            else
            {
                resolvedContextId = contextId;
            }
            _logger.LogInformation("received request chat with OpenAI model [contextId: {}, modelName: {}, sessionId: {}]", resolvedContextId, modelName, sessionId);
            return ExecuteWithExceptionHandler(() =>
            {
                var response = _openAIChatService.ChatWithAIModel(modelName, "unknown", sessionId, 150, message);

                _logger.LogInformation("completed request chat with OpenAI model [contextId: {}, modelName: {}, sessionId: {}]", resolvedContextId, modelName, sessionId);
                return Ok(response);
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
        public IActionResult CreateModel([FromBody] CreateOpenAIModelRequest completionRequest, [FromQuery] string contextId = "")
        {
            var timer = Timer.Timer.TimerFactory(true);
            var resolvedContextId = "";

            if (contextId == null || contextId == "")
            {
                resolvedContextId = Guid.NewGuid().ToString();
            }
            else
            {
                resolvedContextId = contextId;
            }
            _logger.LogInformation("received request to create OpenAI text completion model [contextId: {}, modelName: {}]", resolvedContextId, completionRequest.ModelName);
            return ExecuteWithExceptionHandler(() =>
            {
                var response = _openAIModelService.CreateCompletionModel(completionRequest.ModelName,
                    completionRequest.ModelRaw,
                    completionRequest.ModelAuthor,
                    completionRequest.ModelStop,
                    completionRequest.ModelType);

                _logger.LogInformation("completed successfully request to create OpenAI text completion model [contextId: {}, modelName: {}, timeTaken: {}]", resolvedContextId, completionRequest.ModelName, timer.GetTimeElasped());
                return Created(".", response);
            });
        }
    }
}

