namespace TRS.AI.MistralAI.Interfaces;

public interface IMistralAIService
{
    public IModelService Models { get; }

    public IChatCompletionService ChatCompletion { get; }

    public IEmbeddingService Embeddings { get; }

    void SetDefaultModelId(string modelId);
}
