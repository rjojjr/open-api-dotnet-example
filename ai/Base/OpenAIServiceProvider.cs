using System;
using OpenAI.GPT3;
using OpenAI.GPT3.Managers;

namespace open_ai_example.ai.Base
{
	public class OpenAIServiceProvider
	{

        private OpenAIService _openAIService;

		public OpenAIServiceProvider()
		{
            _openAIService = new OpenAIService(new OpenAiOptions()
            {
                ApiKey = Environment.GetEnvironmentVariable("OPEN_AI_API_KEY")
            });
        }

        public OpenAIService Get()
        {
            return _openAIService;
        }
	}
}

