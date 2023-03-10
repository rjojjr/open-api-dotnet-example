using System;
using Microsoft.Extensions.DependencyInjection;
using open_ai_example.ai.Base;
using open_ai_example.Model;
using OpenAI.GPT3;
using OpenAI.GPT3.Interfaces;
using OpenAI.GPT3.Managers;
using OpenAI.GPT3.ObjectModels;
using OpenAI.GPT3.ObjectModels.RequestModels;
using OpenAI.GPT3.ObjectModels.ResponseModels;

namespace open_ai_example.ai.Completions
{
	public class OpenAICompletionService
	{

        private OpenAIService _openAIService;

        public OpenAICompletionService(OpenAIServiceProvider openAIServiceProvider)
        {
            _openAIService = openAIServiceProvider.Get();
        }

        // Cost: 10 == Davinci, 9 == curie
        public TextCompletionResponse GetCompletion(string prompt, int maxTokens = 1, int cost = 10, float temperature = 0.49f)
        {
            return _completion(prompt, maxTokens, cost, temperature).Result;
        }

        private async Task<TextCompletionResponse> _completion(string prompt, int maxTokens, int cost, float temperature)
		{
            var timer = Timer.Timer.TimerFactory(true);

            //return new TextCompletionResponse("", timer.GetTimeElasped());

            var completionResult = await _openAIService.Completions.CreateCompletion(new CompletionCreateRequest()
            {
                Prompt = prompt,
                MaxTokens = maxTokens,
                Stop = "Human:,AI:",
                Temperature = temperature,
                TopP = 1,
                PresencePenalty = 0.6f,
                FrequencyPenalty = 0,
                BestOf = 1
            }, cost == 10 ? Models.TextDavinciV3 : Models.TextCurieV1);

            var unknownError = new Exception("Error while processing completion result");
            if (completionResult.Successful)
            {
                var result = completionResult.Choices.FirstOrDefault();
                if (result == null)
                {
                    throw unknownError;
                }
                Console.WriteLine(result);

                return new TextCompletionResponse(result.Text, timer.GetTimeElasped());
            }
            else
            {
                if (completionResult.Error == null)
                {
                    throw unknownError;
                }
                Console.WriteLine($"{completionResult.Error.Code}: {completionResult.Error.Message}");
                return new TextCompletionResponse($"{completionResult.Error.Code}: {completionResult.Error.Message}", timer.GetTimeElasped());
            }
        }
	}
}

