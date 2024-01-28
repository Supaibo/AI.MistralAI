using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using TRS.AI.MistralAI.Interfaces;
using TRS.AI.MistralAI.ObjectModels.SharedModels;

namespace TRS.AI.MistralAI.ObjectModels.RequestModels;

/// <summary>
/// Chat Completion Create Request
/// </summary>
public class ChatCompletionCreateRequest : IModelValidate, ITemperature, IModel
{
    /// <summary>
    ///     The prompt(s) to generate completions for, encoded as a list of dict with role and content. The first prompt role should be user or system.
    /// </summary>
    [JsonPropertyName("messages")]
    public IList<ChatMessage> Messages { get; set; }

    /// <summary>
    ///     Nucleus sampling, where the model considers the results of the tokens with top_p probability mass. So 0.1 means only the tokens comprising the top 10% probability mass are considered.
    ///      We generally recommend altering this or temperature but not both.
    /// </summary>
    [JsonPropertyName("top_p")]
    public float? TopP { get; set; }

    /// <summary>
    ///     Whether to stream back partial progress. If set, tokens will be sent as data-only server-sent events as they become available, with the stream terminated by a data: [DONE] message. 
    ///     Otherwise, the server will hold the request open until the timeout or until completion, with the response containing the full result as JSON.
    /// </summary>
    [JsonPropertyName("stream")]
    public bool? Stream { get; set; }

    /// <summary>
    /// Whether to inject a safety prompt before all conversations.
    /// </summary>
    [JsonPropertyName("safe_prompt")]
    public bool SafePrompt { get; set; }

    /// <summary>
    ///     The maximum number of tokens to generate in the completion.
    ///     The token count of your prompt plus max_tokens cannot exceed the model's context length. 
    /// </summary>
    [JsonPropertyName("max_tokens")]
    public int? MaxTokens { get; set; }

    /// <summary>
    ///     The seed to use for random sampling. If set, different calls will generate deterministic results.
    /// </summary>
    [JsonPropertyName("random_seed")]
    public int? Seed { get; set; }

    /// <summary>
    ///     ID of the model to use. You can use the List Available Models API to see all of your available models, or see our Model overview for model descriptions.
    /// </summary>
    [JsonPropertyName("model")]
    public string? Model { get; set; }

    /// <summary>
    ///     What sampling temperature to use, between 0.0 and 1.0. Higher values like 0.8 will make the output more random, while lower values like 0.2 will make it more focused and deterministic.
    ///     We generally recommend altering this or top_p but not both.
    /// </summary>
    [JsonPropertyName("temperature")]
    public float? Temperature { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public IEnumerable<ValidationResult> Validate()
    {
        throw new NotImplementedException();
    }
}
