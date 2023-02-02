using System;
using open_ai_example.Model;
using open_ai_example.Repository;

namespace open_ai_example.ai.Completions
{
	public class OpenAIModelService
	{

        private readonly ILogger<OpenAIModelService> _logger;
        private readonly ModelRepository _modelRepository;

        public OpenAIModelService(ILogger<OpenAIModelService> logger, ModelRepository modelRepository)
        {
            _logger = logger;
            _modelRepository = modelRepository;
        }

        public OpenAICompletionModel CreateCompletionModel(string modelName, string modelRaw, string modelAuthor, string modelStop, int costLevel, ModelType modelType)
        {
            var time = DateTime.UtcNow;
            var model = new OpenAICompletionModel();
            model.ModelName = modelName;
            model.ModelRaw = modelRaw;
            model.ModelAuthor = modelAuthor;
            model.ModelStop = modelStop;
            model.ModelType = modelType;
            model.CurrentCostLevel = costLevel;
            model.CreatedAt = time;
            model.ModifiedAt = time;

            model.revisions.Add(model.ToRevision());

            _modelRepository.CreateAsync(model).Wait();

            return model;
        }

        public OpenAICompletionModel GetModelByName(string modelName)
        {
            var model = _modelRepository.FindByModelName(modelName);
            if (model == null)
            {
                throw new Exception("model " + modelName + " does not exist");
            }

            return model;
        }
    }
}

