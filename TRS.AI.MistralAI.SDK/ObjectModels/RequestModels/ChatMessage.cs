using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TRS.AI.MistralAI.ObjectModels.RequestModels;

public class ChatMessage
{
    public ChatMessage()
    {
    }

    public ChatMessage(string role, string content, string? name = null)
    {
        Role = role;
        Content = content;
        Name = name;
    }

    /// <summary>
    ///     The role of the author of this message. One of system, user, or assistant.
    /// </summary>
    [JsonPropertyName("role")]
    public string Role { get; set; }

    [JsonIgnore] public string? Content { get; set; }

    /// <summary>
    ///     The contents of the message.
    /// </summary>
    [JsonPropertyName("content")]
    public object ContentCalculated
    {
        get
        {
            if (Content is null)
            {
                throw new ValidationException(
                    "Content is null."
                );
            }
            
            return Content;
        }
        set => Content = value?.ToString();
    }

    /// <summary>
    ///     The name of the author of this message. May contain a-z, A-Z, 0-9, and underscores, with a maximum length of 64
    ///     characters.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    public static ChatMessage FromAssistant(string content, string? name = null)
    {
        return new(StaticValues.ChatMessageRoles.Assistant, content, name);
    }

    public static ChatMessage FromUser(string content, string? name = null)
    {
        return new(StaticValues.ChatMessageRoles.User, content, name);
    }

    public static ChatMessage FromSystem(string content, string? name = null)
    {
        return new(StaticValues.ChatMessageRoles.System, content, name);
    }
}
