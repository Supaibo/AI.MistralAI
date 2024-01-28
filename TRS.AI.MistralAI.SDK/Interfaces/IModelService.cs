using TRS.AI.MistralAI.ObjectModels.ResponseModels.ModelResponseModels;

namespace TRS.AI.MistralAI.Interfaces
{
    public interface IModelService
    {
        Task<ModelListResponse> ListModel(CancellationToken cancellationToken = default);
    }
}
