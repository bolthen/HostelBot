using System.ComponentModel.DataAnnotations;
using System.Reflection;
using HostelBot.Domain.Infrastructure;
using HostelBot.Domain.Infrastructure.Exceptions;

namespace HostelBot.Ui.TelegramBot.Handlers.Filling;

internal class FillingProgress
{
    private int Stage { get; set; } = 0;
    private bool Completed => Stage == properties.Length;

    private readonly PropertyInfo[] properties;

    public readonly Dictionary<string, string> Answers = new();

    public readonly IFillable fillable;
    public bool IsRegistration { get; }

    public FillingProgress(IFillable fillable, long chatId, bool isRegistration = false)
    {
        IsRegistration = isRegistration;
        
        this.fillable = fillable;
        properties = fillable
            .GetFields()
            .Where(propertyInfo => propertyInfo.GetCustomAttribute<QuestionAttribute>() != null)
            .ToArray();
        
        ChatId2FillingProgress[chatId] = this;
    }

    private void SaveResponse(string response)
    {
        var key = properties[Stage].Name;
        Answers[key] = response;
    }

    private bool TryValidateRegex(string response, out string? errorMessage)
    {
        errorMessage = null;
        
        var regex = properties[Stage].GetCustomAttribute<RegularExpressionAttribute>();
        if (regex is null || regex.IsValid(response))
            return true;

        errorMessage = regex.ErrorMessage;
        return false;
    }

    public string GetNextQuestion()
    {
        return properties[Stage].GetCustomAttribute<QuestionAttribute>()!.Question;
    }

    public (CurrentProgressStatus progressStatus, string? errorMessage) HandleResponse(string text)
    {
        if (!TryValidateRegex(text, out var errorMessage))
            return (CurrentProgressStatus.RegexFailed, errorMessage);
        
        SaveResponse(text);
        Stage++;

        if (Completed)
            return (CurrentProgressStatus.Completed, null);
        
        return (CurrentProgressStatus.WrittenDown, null);
    }


    private static readonly Dictionary<long, FillingProgress> ChatId2FillingProgress = new();
    
    public static bool IsUserCurrentlyFilling(long chatId)
    {
        return ChatId2FillingProgress.ContainsKey(chatId);
    }

    public static FillingProgress GetFillingProgress(long chatId)
    {
        return ChatId2FillingProgress[chatId];
    }
    
    public static void FinishFilling(long chatId)
    {
        ChatId2FillingProgress.Remove(chatId);
    }

    public enum CurrentProgressStatus
    {
        WrittenDown, RegexFailed, Completed
    }
}