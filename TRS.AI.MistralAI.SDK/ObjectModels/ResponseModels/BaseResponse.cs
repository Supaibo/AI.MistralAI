using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TRS.AI.MistralAI.ObjectModels.ResponseModels;

public record BaseResponse
{
    [JsonPropertyName("object")] public string? ObjectTypeName { get; set; }
    public bool Successful => Error == null;
    [JsonPropertyName("error")] public Error? Error { get; set; }
    public HttpStatusCode HttpStatusCode { get; set; }
    public ResponseHeaderValues? HeaderValues { get; set; }
}

public record RateLimitInfo
{
    public string? LimitMonths { get; set; }
    public string? LimitMinute { get; set; }
    public string? RemainingMinute { get; set; }
    public string? Reset { get; set; }
    public string? RemainingMonths { get; set; }
    public string? QueryCost { get; set; }
    public string? Limit { get; set; }
    public string? Remaining { get; set; }
}

public record KongInfo
{
    public string? Latency { get; set; }
    public string? UpstreamLatency { get; set; }
    public string? RequestId { get; set; }
}

public record ResponseHeaderValues
{
    public DateTimeOffset? Date { get; set; }
    public string? Connection { get; set; }
    public string? AccessControlAllowOrigin { get; set; }
    public string? XKongRequestId { get; set; }
    public string? CFCacheStatus { get; set; }
    public string? Server { get; set; }
    public string? CF_RAY { get; set; }
    public string? AltSvc { get; set; }
    public Dictionary<string, IEnumerable<string>>? All { get; set; }

    public RateLimitInfo? RateLimits { get; set; }
    public KongInfo? Kong { get; set; }
}

public record DataBaseResponse<T> : BaseResponse
{
    [JsonPropertyName("data")] public T? Data { get; set; }
}

public class Error
{
    [JsonPropertyName("code")] public string? Code { get; set; }

    [JsonPropertyName("param")] public string? Param { get; set; }

    [JsonPropertyName("type")] public string? Type { get; set; }

    [JsonIgnore] public string? Message { get; private set; }

    [JsonIgnore] public List<string?> Messages { get; private set; }

    [JsonPropertyName("message")]
    [JsonConverter(typeof(MessageConverter))]
    public object MessageObject
    {
        set
        {
            switch (value)
            {
                case string s:
                    Message = s;
                    Messages = new() { s };
                    break;
                case List<object> list when list.All(i => i is JsonElement):
                    Messages = list.Cast<JsonElement>().Select(e => e.GetString()).ToList();
                    Message = string.Join(Environment.NewLine, Messages);
                    break;
            }
        }
    }

    public class MessageConverter : JsonConverter<object>
    {
        public override object? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                return reader.GetString();
            }

            if (reader.TokenType == JsonTokenType.StartArray)
            {
                return JsonSerializer.Deserialize<List<object>>(ref reader, options);
            }

            throw new JsonException();
        }

        public override void Write(Utf8JsonWriter writer, object value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value, options);
        }
    }
}
