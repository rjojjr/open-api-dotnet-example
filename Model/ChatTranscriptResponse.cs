using System;
using open_ai_example.ai.Completions.Transcripts;

namespace open_ai_example.Model
{
	public class ChatTranscriptResponse
	{

		public string SessionId { get; set; } = null!;
		public IList<ChatTranscriptEntity> Transcripts { get; set; } = new List<ChatTranscriptEntity>();

		public ChatTranscriptResponse()
		{
		}

        public ChatTranscriptResponse(string sessionId, IList<ChatTranscriptEntity> transcripts)
        {
            SessionId = sessionId;
            Transcripts = transcripts;
        }
    }
}

