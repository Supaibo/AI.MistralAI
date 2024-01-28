namespace TRS.AI.MistralAI.EndpointProviders;

internal interface IMistralAIEndpointProvider
{
    string ChatCompletionCreate();
    string EmbeddingCreate();
    string ModelsList();
}
