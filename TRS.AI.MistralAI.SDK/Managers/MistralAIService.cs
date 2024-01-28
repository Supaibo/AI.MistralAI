using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using TRS.AI.MistralAI.EndpointProviders;
using TRS.AI.MistralAI.Interfaces;

namespace TRS.AI.MistralAI.Managers;

public partial class MistralAIService : IMistralAIService, IDisposable
{
    private readonly bool _disposeHttpClient;
    private readonly IMistralAIEndpointProvider _endpointProvider;
    private readonly HttpClient _httpClient;
    private string? _defaultModelId;

    [ActivatorUtilitiesConstructor]
    public MistralAIService(IOptions<MistralAIOptions> settings, HttpClient httpClient)
: this(settings.Value, httpClient)
    {
    }

    public MistralAIService(MistralAIOptions settings, HttpClient? httpClient = null)
    {
        settings.Validate();

        if (httpClient == null)
        {
            _disposeHttpClient = true;
            _httpClient = new HttpClient();
        }
        else
        {
            _httpClient = httpClient;
        }

        _httpClient.BaseAddress = new Uri(settings.BaseDomain);

        switch (settings.ProviderType)
        {
            case ProviderType.MistralAi:
            default:
                _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {settings.ApiKey}");
                break;
        }

        _endpointProvider = settings.ProviderType switch
        {
            _ => new MistralAiEndpointProvider(settings.ApiVersion)
        };

        _defaultModelId = settings.DefaultModelId;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <inheritdoc />
    public IModelService Models => this;

    /// <inheritdoc />
    public IChatCompletionService ChatCompletion => this;

    /// <inheritdoc />
    public IEmbeddingService Embeddings => this;

    public void SetDefaultModelId(string modelId)
    {
        _defaultModelId = modelId;
    }

    public string? GetDefaultModelId()
    {
        return _defaultModelId;
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (_disposeHttpClient && _httpClient != null)
            {
                _httpClient.Dispose();
            }
        }
    }
}
