# Dotnet SDK for MistralAI

```
Install-Package TRS.AI.MistralAI
```

Dotnet SDK for Mistral AI  
*Unofficial*. 
*MistralAI doesn't have any official .Net SDK.*

## Features
- [x] [Completions](https://docs.mistral.ai/)
- [x] [Embeddings](https://docs.mistral.ai/)


For changelogs please go to end of the document.

## Sample Usages

Your API Key comes from here --> https://mistral.ai/  

### Without using dependency injection:
```csharp
var aiService = new MistralAIService(new MistralAIOptions()
{
    ApiKey =  Environment.GetEnvironmentVariable("MISTRAL_API_KEY")
});
```
### Using dependency injection:
#### secrets.json: 

```json
 "MistralAIServiceOptions": {
    //"ApiKey":"Your api key goes here"
  }
```
*(How to use [user secret](https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-6.0&tabs=windows) ?  
Right click your project name in "solution explorer" then click "Manage User Secret", it is a good way to keep your api keys)*

#### Program.cs
```csharp
serviceCollection.AddMistralAIService();
```

**OR**  
Use it like below but do NOT put your API key directly to your source code. 
#### Program.cs
```csharp
serviceCollection.AddMistralAIService(settings => { settings.ApiKey = Environment.GetEnvironmentVariable("MISTRAL_API_KEY"); });
```

After injecting your service you will be able to get it from service provider
```csharp
var aiService = serviceProvider.GetRequiredService<IMistralAIService>();
```

You can set default model(optional):
```csharp
aiService.SetDefaultModelId(Models.Mistral_Medium);
```

## Notes

## Changelog
### 0.0.1
- Implementation of completion API
- Implementation of embeddings API
- Implementation of models API