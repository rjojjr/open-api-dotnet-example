using System;
namespace open_ai_example.Model
{
	public class CreateOpenAIModelRequest
	{
        public string ModelId { get; set; } = null!;
        public string ModelName { get; set; } = null!;
        public string ModelRaw { get; set; } = null!;
        public string ModelAuthor { get; set; } = null!;
        public string ModelStop { get; set; } = null!;
        public int CostLevel { get; set; }
        // Controls randomness. Values between 0 and 1, 0 is not random at all.
        public float Temperature { get; set; } = 0.49f;
        public ModelType ModelType { get; set; }

        public CreateOpenAIModelRequest()
		{
		}

        public CreateOpenAIModelRequest(string modelName, string modelRaw, string modelAuthor, string modelStop, int costLevel, float temperature, ModelType modelType)
        {
            ModelName = modelName;
            ModelRaw = modelRaw;
            ModelAuthor = modelAuthor;
            ModelStop = modelStop;
            CostLevel = costLevel;
            ModelType = modelType;
            Temperature = temperature;
        }

        public CreateOpenAIModelRequest(string modelId, string modelName, string modelRaw, string modelAuthor, string modelStop, int costLevel, float temperature, ModelType modelType)
        {
            ModelId = modelId;
            ModelName = modelName;
            ModelRaw = modelRaw;
            ModelAuthor = modelAuthor;
            ModelStop = modelStop;
            CostLevel = costLevel;
            ModelType = modelType;
            Temperature = temperature;
        }
    }
}

