using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using TRS.AI.MistralAI.Interfaces;
using TRS.AI.MistralAI.ObjectModels.SharedModels;

namespace TRS.AI.MistralAI.ObjectModels.RequestModels;

public record EmbeddingCreateRequest : IModelValidate, IModel
{
    [JsonIgnore]
    public List<string>? InputAsList { get; set; }

    [JsonIgnore]
    public string? Input { get; set; }

    [JsonPropertyName("input")]
    public IList<string>? InputCalculated
    {
        get
        {
            if (Input != null && InputAsList != null)
            {
                throw new ValidationException("Input and InputAsList can not be assigned at the same time. One of them is should be null.");
            }

            if (Input != null)
            {
                return new List<string> { Input };
            }

            return InputAsList;
        }
    }

    [JsonPropertyName("model")]
    public string? Model { get; set; }

    [JsonPropertyName("encoding_format")]
    public string? Encoding { get; set; }

    public IEnumerable<ValidationResult> Validate()
    {
        throw new NotImplementedException();
    }
}
