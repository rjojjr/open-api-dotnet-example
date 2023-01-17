using System;
namespace open_ai_example.Model
{
	public class TextCompletionResponse
	{

		public string Response;
		public long TimeTaken = 0;

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

