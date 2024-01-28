using TRS.AI.MistralAI.ObjectModels.RequestModels;
using TRS.AI.MistralAI.ObjectModels.ResponseModels;
using static TRS.AI.MistralAI.ObjectModels.Models;

namespace TRS.AI.MistralAI.Interfaces;

public interface IChatCompletionService
{
    Task<ChatCompletionCreateResponse> CreateCompletion(ChatCompletionCreateRequest chatCompletionCreate, string? modelId = null, CancellationToken cancellationToken = default);

    IAsyncEnumerable<ChatCompletionCreateResponse> CreateCompletionAsStream(ChatCompletionCreateRequest chatCompletionCreate, string? modelId = null, CancellationToken cancellationToken = default);
}

public static class IChatCompletionServiceExtension
{
    /// <summary>
    ///     Creates a new completion for the provided prompt and parameters
    /// </summary>
    /// <param name="service"></param>
    /// <param name="chatCompletionCreate"></param>
    /// <param name="modelId">The ID of the model to use for this request</param>
    /// <param name="cancellationToken">Propagates notification that operations should be canceled.</param>
    /// <returns></returns>
    public static Task<ChatCompletionCreateResponse> Create(this IChatCompletionService service, ChatCompletionCreateRequest chatCompletionCreate, Model modelId, CancellationToken cancellationToken = default)
    {
        return service.CreateCompletion(chatCompletionCreate, modelId.EnumToString(), cancellationToken);
    }
}
