using TRS.AI.MistralAI.Interfaces;
using TRS.AI.MistralAI.ObjectModels;
using TRS.AI.MistralAI.ObjectModels.RequestModels;

namespace TRS.AI.MistralAI.Playground.TestHelpers
{
    internal static class EmbeddingTestHelper
    {
        public static async Task RunSimpleEmbeddingTest(IMistralAIService sdk)
        {
            ConsoleExtensions.WriteLine("Simple Embedding test is starting:", ConsoleColor.Cyan);

            try
            {
                ConsoleExtensions.WriteLine("Embedding Test:", ConsoleColor.DarkCyan);
                var embeddingResult = await sdk.Embeddings.CreateEmbedding(new EmbeddingCreateRequest
                {
                    InputAsList = new List<string> { "The quick brown fox jumped over the lazy dog." },
                    Model = Models.Mistral_Embed
                });

                if (embeddingResult.Successful)
                {
                    Console.WriteLine(embeddingResult.Data.FirstOrDefault());
                }
                else
                {
                    if (embeddingResult.Error == null)
                    {
                        throw new Exception("Unknown Error");
                    }

                    Console.WriteLine($"{embeddingResult.Error.Code}: {embeddingResult.Error.Message}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
