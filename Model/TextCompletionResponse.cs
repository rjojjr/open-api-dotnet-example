using System;
namespace open_ai_example.Model
{
    public class TextCompletionResponse
    {

        public string Response { get; set; } = null!;
        public long TimeTaken { get; set; }

        public TextCompletionResponse(string response, long timeTaken)
        {
            Response = response;
            TimeTaken = timeTaken;
        }

        public TextCompletionResponse()
        {
        }
    }
}

