﻿using System;
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
        public ModelType ModelType { get; set; }

        public CreateOpenAIModelRequest()
		{
		}

        public CreateOpenAIModelRequest(string modelName, string modelRaw, string modelAuthor, string modelStop, int costLevel, ModelType modelType)
        {
            ModelName = modelName;
            ModelRaw = modelRaw;
            ModelAuthor = modelAuthor;
            ModelStop = modelStop;
            CostLevel = costLevel;
            ModelType = modelType;
        }

        public CreateOpenAIModelRequest(string modelId, string modelName, string modelRaw, string modelAuthor, string modelStop, int costLevel, ModelType modelType)
        {
            ModelId = modelId;
            ModelName = modelName;
            ModelRaw = modelRaw;
            ModelAuthor = modelAuthor;
            ModelStop = modelStop;
            CostLevel = costLevel;
            ModelType = modelType;
        }
    }
}

