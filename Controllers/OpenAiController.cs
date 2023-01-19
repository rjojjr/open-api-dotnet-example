using System;
using Microsoft.AspNetCore.Mvc;
using open_ai_example.Timer;
using open_ai_example.ai.Completions;

namespace open_ai_example.Controllers
{

    [ApiController]
    [Route("open-ai/api/v1")]
    public class OpenAiController : BaseController
	{

        private readonly ILogger<OpenAiController> _logger;
        private readonly OpenAICompletionService _openAiCompletionService;

        public OpenAiController(ILogger<OpenAiController> logger,
            OpenAICompletionService openAiCompletionService)
        {
            _logger = logger;
            _openAiCompletionService = openAiCompletionService;
        }

        /// <summary>
        /// Processes given completion prompt.
        /// </summary>
        /// <remarks>Process completion for given prompt.</remarks>
        /// <response code="200">Success</response>
        /// <response code="400">Prompt is a required query parameter</response>
        /// <response code="500">Something went wrong</response>
        [HttpGet("completion")]
        public IActionResult GetCompletion([FromQuery] string prompt = "",
            [FromQuery] int maxTokens = 1,
            [FromQuery] string contextId = "")
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
                //if (prompt == null || prompt == "")
                //{
                //    _logger.LogInformation("completed usuccessfully request to perform OpenAI text completion [contextId: {}, timeTaken: {}]", resolvedContextId, timer.GetTimeElasped());
                //    return BadRequest("prompt is required.");
                //}

                _logger.LogInformation("completed successfully request to perform OpenAI text completion [contextId: {}, timeTaken: {}]", resolvedContextId, timer.GetTimeElasped());
                return Ok(_openAiCompletionService.GetCompletion(prompt, maxTokens));
            });
        }
    }
}

