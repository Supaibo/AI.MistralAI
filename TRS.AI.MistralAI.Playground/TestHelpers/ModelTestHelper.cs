using TRS.AI.MistralAI.Interfaces;

namespace TRS.AI.MistralAI.Playground.TestHelpers
{
    internal static class ModelTestHelper
    {
        public static async Task FetchModelsTest(IMistralAIService sdk)
        {
            ConsoleExtensions.WriteLine("Model List Testing is starting:", ConsoleColor.Cyan);

            try
            {
                ConsoleExtensions.WriteLine("Fetching Model List", ConsoleColor.DarkCyan);
                var modelList = await sdk.Models.ListModel();
                if (modelList == null)
                {
                    ConsoleExtensions.WriteLine("Fetching Model List failed", ConsoleColor.DarkRed);
                    throw new NullReferenceException(nameof(modelList));
                }

                ConsoleExtensions.WriteLine("Models:", ConsoleColor.DarkGreen);
                Console.WriteLine(string.Join(Environment.NewLine, modelList.Models.Select(r => r.Id)));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
