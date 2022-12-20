using System.ComponentModel.DataAnnotations;
using System.Reflection;
using HostelBot.App;
using HostelBot.Domain.Infrastructure;

namespace HostelBot.Ui.TelegramBot;

public class FillingProgress
{
    private int Stage { get; set; } = -1;

    public bool Completed => Stage == properties.Length - 1;

    private readonly PropertyInfo[] properties;

    public readonly Dictionary<string, string> Answers = new();

    private readonly IFillable fillable;
    public Command Command { get; }

    public FillingProgress(IFillable fillable, long chatId, Command command)
    {
        Command = command;
        
        this.fillable = fillable;
        properties = fillable
            .GetFields()
            .Where(propertyInfo => propertyInfo.GetCustomAttribute<QuestionAttribute>() != null)
            .ToArray();
        
        ChatId2FillingProgress[chatId] = this;
    }

    public void SaveResponse(string response)
    {
        var key = properties[Stage].Name;
        Answers[key] = response.Trim();
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
    
    
    private static readonly Dictionary<long, FillingProgress> ChatId2FillingProgress = new();
    
    public static bool IsUserCurrentlyFilling(long chatId)
    {
        return ChatId2FillingProgress.ContainsKey(chatId);
    }

    public static FillingProgress GetProgress(long chatId)
    {
        return ChatId2FillingProgress[chatId];
    }
    
    public static void FinishFilling(long chatId)
    {
        var progress = GetProgress(chatId);
        progress.fillable.FillClass(progress.Answers);
        ChatId2FillingProgress.Remove(chatId);
    }
}