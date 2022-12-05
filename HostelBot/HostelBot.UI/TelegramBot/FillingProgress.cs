using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text.Json.Serialization;
using HostelBot.Domain.Infrastructure;

namespace HostelBot.Ui.TelegramBot;

public class FillingProgress
{
    private int Stage { get; set; } = -1;

    public bool Completed => Stage == properties.Length - 1;

    private readonly PropertyInfo[] properties;

    public readonly Dictionary<string, string> Result = new();

    public readonly IFillable Fillable;

    public FillingProgress(IFillable fillable, long chatId)
    {
        Fillable = fillable;
        properties = fillable
            .GetFields()
            .Where(propertyInfo => propertyInfo.GetCustomAttribute<QuestionAttribute>() != null)
            .ToArray();
        
        ChatIdToFillingProgress[chatId] = this;
    }

    public void SaveResponse(string response)
    {
        var key = properties[Stage].GetCustomAttribute<JsonPropertyNameAttribute>()!.Name;
        Result[key] = response.Trim();
    }

    public bool TryValidateRegex(string response, out string? errorMessage)
    {
        errorMessage = null;
        
        var regex = properties[Stage].GetCustomAttribute<RegularExpressionAttribute>();
        if (regex is null || regex.IsValid(response))
            return true;

        errorMessage = regex.ErrorMessage;
        return false;
    }

    public string? GetNextQuestion()
    {
        if (Completed)
            return null;

        Stage++;

        return properties[Stage].GetCustomAttribute<QuestionAttribute>()!.Question;
    }
    
    
    private static readonly Dictionary<long, FillingProgress> ChatIdToFillingProgress = new();
    
    public static bool IsUserCurrentlyFilling(long chatId)
    {
        return ChatIdToFillingProgress.ContainsKey(chatId);
    }

    public static FillingProgress GetProgress(long chatId)
    {
        return ChatIdToFillingProgress[chatId];
    }
    
    public static void FinishFilling(long chatId)
    {
        var progress = GetProgress(chatId);
        progress.Fillable.FillClass(progress.Result);
        ChatIdToFillingProgress.Remove(chatId);
    }
}