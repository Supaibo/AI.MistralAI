using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TRS.AI.MistralAI.ObjectModels.SharedModels;

namespace TRS.AI.MistralAI.ObjectModels.ResponseModels;

public record ChatCompletionCreateResponse : BaseResponse, IId, ICreatedAt
{
    [JsonPropertyName("model")] public string Model { get; set; }

    [JsonPropertyName("choices")] public List<ChatChoiceResponse> Choices { get; set; }

    [JsonPropertyName("usage")] public UsageResponse Usage { get; set; }

    [JsonPropertyName("created")] public int CreatedAt { get; set; }

    [JsonPropertyName("id")] public string Id { get; set; }

    [JsonPropertyName("system_fingerprint")] public string SystemFingerPrint { get; set; }
}
