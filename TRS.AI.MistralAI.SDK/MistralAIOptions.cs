namespace TRS.AI.MistralAI;

/// <summary>
///     Provider Type
/// </summary>
public enum ProviderType
{
    /// <summary>
    ///     Mistral Provider
    /// </summary>
    MistralAi = 1
}

public class MistralAIOptions
{
    private const string MistralAiDefaultApiVersion = "v1";
    private const string MistralAiDefaultBaseDomain = "https://api.mistral.ai/";

    /// <summary>
    ///     Setting key for Json Setting Bindings
    /// </summary>
    public static readonly string SettingKey = "MistralAIServiceOptions";

    private string? _apiVersion;
    private string? _baseDomain;

    /// <summary>
    ///     Get Provider Type
    /// </summary>
    public ProviderType ProviderType { get; set; } = ProviderType.MistralAi;

    public string ApiKey { get; set; } = null!;

    public string ApiVersion
    {
        get
        {
            return _apiVersion ??= ProviderType switch
            {
                ProviderType.MistralAi => MistralAiDefaultApiVersion,
                _ => throw new ArgumentOutOfRangeException(nameof(ProviderType))
            };
        }
        set => _apiVersion = value;
    }

    /// <summary>
    ///     Base Domain
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public string BaseDomain
    {
        get
        {
            //we can ignore possible null value until resourceName is set
#pragma warning disable CS8603
            return _baseDomain ??= ProviderType switch
            {
                ProviderType.MistralAi => MistralAiDefaultBaseDomain,
                _ => throw new ArgumentOutOfRangeException(nameof(ProviderType))
            };
#pragma warning restore CS8603
        }
        set => _baseDomain = value;
    }

    public bool ValidateApiOptions { get; set; } = true;

    public string? DefaultModelId { get; set; }

    public void Validate()
    {
        if (!ValidateApiOptions)
        {
            return;
        }

        if (string.IsNullOrEmpty(ApiKey))
        {
            throw new ArgumentNullException(nameof(ApiKey));
        }

        if (string.IsNullOrEmpty(ApiVersion))
        {
            throw new ArgumentNullException(nameof(ApiVersion));
        }

        if (string.IsNullOrEmpty(BaseDomain))
        {
            throw new ArgumentNullException(nameof(BaseDomain));
        }
    }
}

