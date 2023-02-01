using System;
namespace open_ai_example.ai.Completions.Transcripts
{

	public class ChatTranscriptEntry
	{
		public string Id { get; set; } = null!;
        public ChatParticipantType ParticipantType { get; set; }
		public string ParticipantName { get; set; } = null!;
        public DateTime SentAt { get; set; }
		public string Message { get; set; } = null!;

		public ChatTranscriptEntry()
		{
            this.Id = Guid.NewGuid().ToString();
        }
    }

	public class ChatTranscriptEntity
	{
        public string Id { get; set; }
        public string ModelId { get; set; }
		public int ModelRevision { get; set; }
        public DateTime StartedAt { get; set; }
		public IList<ChatTranscriptEntry> ChatTranscriptEntries = new List<ChatTranscriptEntry>();
        public ChatTranscriptEntity()
		{
		}
	}
}

