using System;
namespace open_ai_example.Model
{

	public class Choice
	{
        public string Text { get; set; } = null!;
        public int Index { get; set; }
        public string FinishReason { get; set; } = null!;
    }

	public class Usage
	{
		public int PromptTokens { get; set; }
        public int CompetionTokens { get; set; }
        public int TotalTokens { get; set; }
    }

	public class OpenAIResponse
	{

		public string Id { get; set; } = null!;
		public string Model { get; set; } = null!;
		public IList<Choice> Choices { get; set; } = new List<Choice>();
		public Usage usage { get; set; }

        public OpenAIResponse()
		{
		}
	}
}

