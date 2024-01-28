using TRS.AI.MistralAI.Interfaces;
using TRS.AI.MistralAI.ObjectModels;
using TRS.AI.MistralAI.ObjectModels.RequestModels;

namespace TRS.AI.MistralAI.Playground.TestHelpers
{
    internal class ChatCompletionTestHelper
    {
        public static async Task RunSimpleChatCompletionTest(IMistralAIService sdk)
        {
            ConsoleExtensions.WriteLine("Chat Completion Testing is starting:", ConsoleColor.Cyan);

            try
            {
                ConsoleExtensions.WriteLine("Chat Completion Test:", ConsoleColor.DarkCyan);
                var completionResult = await sdk.ChatCompletion.CreateCompletion(new ChatCompletionCreateRequest
                {
                    Messages = new List<ChatMessage>
                    {
                        ChatMessage.FromSystem("You are a helpful assistant."),
                        ChatMessage.FromUser("Who won the world series in 2020?"),
                        ChatMessage.FromAssistant("The Los Angeles Dodgers won the World Series in 2020."),
                        ChatMessage.FromUser("Tell me a story about The Los Angeles Dodgers")
                    },
                    MaxTokens = 150,
                    Model = Models.Mistral_Small
                });

                if (completionResult.Successful)
                {
                    Console.WriteLine(completionResult.Choices.First().Message.Content);
                }
                else
                {
                    if (completionResult.Error == null)
                    {
                        throw new Exception("Unknown Error");
                    }

                    Console.WriteLine($"{completionResult.Error.Code}: {completionResult.Error.Message}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public static async Task RunSimpleCompletionStreamTest(IMistralAIService sdk)
        {
            ConsoleExtensions.WriteLine("Chat Completion Stream Testing is starting:", ConsoleColor.Cyan);
            try
            {
                ConsoleExtensions.WriteLine("Chat Completion Stream Test:", ConsoleColor.DarkCyan);
                var completionResult = sdk.ChatCompletion.CreateCompletionAsStream(new ChatCompletionCreateRequest
                {
                    Messages = new List<ChatMessage>
                    {
                        ChatMessage.FromSystem("You are a helpful assistant."),
                        ChatMessage.FromUser("Who won the world series in 2020?"),
                        ChatMessage.FromAssistant("The Los Angeles Dodgers won the World Series in 2020."),
                        ChatMessage.FromUser("Tell me a story about The Los Angeles Dodgers")
                    },
                    MaxTokens = 150,
                    Model = Models.Mistral_Small
                });

                await foreach (var completion in completionResult)
                {
                    if (completion.Successful)
                    {
                        Console.Write(completion.Choices.First().Message?.Content);
                    }
                    else
                    {
                        if (completion.Error == null)
                        {
                            throw new Exception("Unknown Error");
                        }

                        Console.WriteLine($"{completion.Error.Code}: {completion.Error.Message}");
                    }
                }

                Console.WriteLine("");
                Console.WriteLine("Complete");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
