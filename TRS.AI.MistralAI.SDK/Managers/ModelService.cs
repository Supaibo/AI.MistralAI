using System.Net.Http.Json;
using TRS.AI.MistralAI.Interfaces;
using TRS.AI.MistralAI.ObjectModels.ResponseModels.ModelResponseModels;

namespace TRS.AI.MistralAI.Managers;

public partial class MistralAIService : IModelService
{
    public async Task<ModelListResponse> ListModel(CancellationToken cancellationToken = default)
    {
        return (await _httpClient.GetFromJsonAsync<ModelListResponse>(_endpointProvider.ModelsList(), cancellationToken))!;
    }
}
