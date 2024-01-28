namespace TRS.AI.MistralAI.ObjectModels;

public static class Models
{
    public enum Model
    {
        Mistral_Embed,
        Mistral_Medium,
        Mistral_Tiny,
        Mistral_Small
    }

    public static string Mistral_Embed => "mistral-embed";
    public static string Mistral_Medium => "mistral-medium";
    public static string Mistral_Tiny => "mistral-tiny";
    public static string Mistral_Small => "mistral-small";

    public static string EnumToString(this Model model)
    {
        return model switch
        {
            Model.Mistral_Embed => Mistral_Embed,
            Model.Mistral_Medium => Mistral_Medium,
            Model.Mistral_Tiny => Mistral_Tiny,
            Model.Mistral_Small => Mistral_Small,
            _ => throw new ArgumentOutOfRangeException(nameof(model), model, null)
        };
    }
}
