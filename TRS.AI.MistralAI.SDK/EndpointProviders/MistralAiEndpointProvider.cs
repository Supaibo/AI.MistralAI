namespace TRS.AI.MistralAI.EndpointProviders;

internal class MistralAiEndpointProvider : IMistralAIEndpointProvider
{
    private readonly string _apiVersion;

    public MistralAiEndpointProvider(string apiVersion)
    {
        _apiVersion = apiVersion;
    }

    public string ChatCompletionCreate()
    {
        return $"{_apiVersion}/chat/completions";
    }

    public string EmbeddingCreate()
    {
        return $"{_apiVersion}/embeddings";
    }

    public string ModelsList()
    {
        return $"{_apiVersion}/models";
    }
}
