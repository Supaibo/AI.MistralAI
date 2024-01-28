using TRS.AI.MistralAI.ObjectModels.SharedModels;

namespace TRS.AI.MistralAI.Extensions;

public static class ModelExtension
{
    public static void ProcessModelId(this IModel modelFromObject, string? modelFromParameter, string? defaultModelId)
    {
        modelFromObject.Model = modelFromParameter ?? modelFromObject.Model ?? defaultModelId ?? throw new ArgumentNullException("Model Id");
    }
}
