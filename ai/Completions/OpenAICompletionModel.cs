using System;
using open_ai_example.Model;

namespace open_ai_example.ai.Completions
{
    public class OpenAICompletionModelRevision
    {
        public string Id { get; set; } = null!;
        public string ModelId { get; set; } = null!;
        public string ModelName { get; set; } = null!;
        public string ModelRaw { get; set; } = null!;
        public string ModelAuthor { get; set; } = null!;
        public string ModelStop { get; set; } = null!;
        public int Revision { get; set; }
        public ModelType ModelType { get; set; } = ModelType.CHAT;
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

        public OpenAICompletionModelRevision()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public OpenAICompletionModelRevision(string modelId,
            string modelName,
            string modelRaw,
            string modelAuthor,
            string modelStop,
            int revision,
            ModelType modelType,
            DateTime createdAt,
            DateTime modifiedAt)
        {
            this.Id = Guid.NewGuid().ToString();
            ModelId = modelId;
            ModelName = modelName;
            ModelRaw = modelRaw;
            ModelAuthor = modelAuthor;
            ModelStop = modelStop;
            Revision = revision;
            CreatedAt = createdAt;
            ModifiedAt = modifiedAt;
        }
    }

	public class OpenAICompletionModel
    {

        public string Id { get; set; } = null!;
        public string ModelName { get; set; } = null!;
        public string ModelRaw { get; set; } = null!;
        public string ModelAuthor { get; set; } = null!;
        public string ModelStop { get; set; } = null!;
        public int CurrentRevision { get; set; } = 0;
        public ModelType ModelType { get; set; } = ModelType.CHAT;
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

        public IList<OpenAICompletionModelRevision> revisions = new List<OpenAICompletionModelRevision>();

        public OpenAICompletionModel()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public OpenAICompletionModelRevision ToRevision()
        {
            return new OpenAICompletionModelRevision(this.Id,
                this.ModelName,
                this.ModelRaw,
                this.ModelAuthor,
                this.ModelStop,
                this.CurrentRevision,
                this.ModelType,
                this.CreatedAt,
                this.ModifiedAt);
        }
    }
}

