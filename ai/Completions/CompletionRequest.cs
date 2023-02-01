using System;
namespace open_ai_example.ai.Completions
{

    public class CompletionRequest
    {
        public string Prompt { get; set; } = null!;
        public int MaxTokens { get; set; }
        public CompletionRequest()
        {
        }
    }
}

