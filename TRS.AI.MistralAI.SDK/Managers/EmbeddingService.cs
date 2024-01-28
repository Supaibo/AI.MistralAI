using TRS.AI.MistralAI.Extensions;
using TRS.AI.MistralAI.Interfaces;
using TRS.AI.MistralAI.ObjectModels.RequestModels;
using TRS.AI.MistralAI.ObjectModels.ResponseModels;

namespace TRS.AI.MistralAI.Managers;

public partial class MistralAIService : IEmbeddingService
{
    public async Task<EmbeddingCreateResponse> CreateEmbedding(EmbeddingCreateRequest createEmbeddingRequest, CancellationToken cancellationToken = default)
    {
        return await _httpClient.PostAndReadAsAsync<EmbeddingCreateResponse>(_endpointProvider.EmbeddingCreate(), createEmbeddingRequest, cancellationToken);
    }
}
