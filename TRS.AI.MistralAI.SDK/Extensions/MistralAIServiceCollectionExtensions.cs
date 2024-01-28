using Microsoft.Extensions.DependencyInjection;
using TRS.AI.MistralAI.Interfaces;
using TRS.AI.MistralAI.Managers;

namespace TRS.AI.MistralAI.Extensions;

public static class MistralAIServiceCollectionExtensions
{
    public static IHttpClientBuilder AddMistralAIService(this IServiceCollection services, Action<MistralAIOptions>? setupAction = null)
    {
        var optionsBuilder = services.AddOptions<MistralAIOptions>();
        if (setupAction != null)
        {
            optionsBuilder.Configure(setupAction);
        }
        else
        {
            optionsBuilder.BindConfiguration(MistralAIOptions.SettingKey);
        }

        return services.AddHttpClient<IMistralAIService, MistralAIService>();
    }

    public static IHttpClientBuilder AddMistralAIService<TServiceInterface>(this IServiceCollection services, string name, Action<MistralAIOptions>? setupAction = null)
        where TServiceInterface : class, IMistralAIService
    {
        var optionsBuilder = services.AddOptions<MistralAIOptions>(name);
        if (setupAction != null)
        {
            optionsBuilder.Configure(setupAction);
        }
        else
        {
            optionsBuilder.BindConfiguration($"{MistralAIOptions.SettingKey}:{name}");
        }

        return services.AddHttpClient<TServiceInterface>();
    }
}
