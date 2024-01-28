using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TRS.AI.MistralAI.Extensions;
using TRS.AI.MistralAI.Interfaces;

var builder = new ConfigurationBuilder()
    .AddJsonFile("ApiSettings.json")
    .AddUserSecrets<Program>();

IConfiguration configuration = builder.Build();
var serviceCollection = new ServiceCollection();
serviceCollection.AddScoped(_ => configuration);

serviceCollection.AddMistralAIService();

var serviceProvider = serviceCollection.BuildServiceProvider();
var sdk = serviceProvider.GetRequiredService<IMistralAIService>();

// await ChatCompletionTestHelper.RunSimpleChatCompletionTest(sdk);
// await ChatCompletionTestHelper.RunSimpleCompletionStreamTest(sdk);
// await EmbeddingTestHelper.RunSimpleEmbeddingTest(sdk);
// await ModelTestHelper.FetchModelsTest(sdk);

Console.WriteLine("Press any key to exit...");
Console.ReadKey();