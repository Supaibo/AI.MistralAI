namespace TRS.AI.MistralAI.ObjectModels;

public class StaticValues
{
    public static class CompletionStatics
    {
        public static class ResponseFormat
        {
            public static string Json => "json_object";
            public static string Text => "text";
        }
    }

    public static class ChatMessageRoles
    {
        public static string System => "system";
        public static string User => "user";
        public static string Assistant => "assistant";
    }
}
