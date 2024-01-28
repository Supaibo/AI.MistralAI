namespace TRS.AI.MistralAI.ObjectModels.SharedModels;

public interface IId
{
    string Id { get; set; }
}

public interface IModel
{
    string? Model { get; set; }
}

public interface ITemperature
{
    float? Temperature { get; set; }
}

public interface ICreatedAt
{
    public int CreatedAt { get; set; }
}
