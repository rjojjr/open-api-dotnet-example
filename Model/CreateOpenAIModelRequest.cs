using System;
namespace open_ai_example.Model
{
	public class CreateOpenAIModelRequest
	{

        public string ModelName { get; set; } = null!;
        public string ModelRaw { get; set; } = null!;
        public string ModelAuthor { get; set; } = null!;
        public string ModelStop { get; set; } = null!;
        public ModelType ModelType { get; set; }

        public CreateOpenAIModelRequest()
		{
		}

        public CreateOpenAIModelRequest(string modelName, string modelRaw, string modelAuthor, string modelStop, ModelType modelType)
        {
            ModelName = modelName;
            ModelRaw = modelRaw;
            ModelAuthor = modelAuthor;
            ModelStop = modelStop;
            ModelType = modelType;
        }
    }
}

