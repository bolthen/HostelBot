using System.Reflection;
using System.Text.Json.Serialization;
using HostelBot.Domain.Infrastructure;

namespace HostelBot.Ui.TelegramBot;

public class FillingProgress
{
    private int Stage { get; set; } = -1;

    public bool Completed => Stage == properties.Length - 1;

    private readonly PropertyInfo[] properties;

    public readonly Dictionary<string, string> result = new();

    public FillingProgress(ICanFill fillClass)
    {
        properties = fillClass
            .GetFields()
            .Where(propertyInfo => propertyInfo.GetCustomAttribute<QuestionAttribute>() != null)
            .ToArray();
    }

    public void SaveResponse(string response)
    {
        var key = properties[Stage].GetCustomAttribute<JsonPropertyNameAttribute>()!.Name;
        result[key] = response.Trim();
    }

    public string? GetNextQuestion()
    {
        if (Completed)
            return null;

        Stage++;

        return properties[Stage].GetCustomAttribute<QuestionAttribute>()!.Question;
    }
}