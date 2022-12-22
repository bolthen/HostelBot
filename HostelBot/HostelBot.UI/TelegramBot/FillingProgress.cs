using System.ComponentModel.DataAnnotations;
using System.Reflection;
using HostelBot.App;
using HostelBot.Domain.Infrastructure;
using HostelBot.Domain.Infrastructure.Exceptions;

namespace HostelBot.Ui.TelegramBot;

public class FillingProgress
{
    private int Stage { get; set; } = -1;

    public bool Completed => Stage == properties.Length - 1;

    private readonly PropertyInfo[] properties;

    public readonly Dictionary<string, string> Answers = new();

    public readonly IFillable fillable;
    public bool IsVerification { get; }

    public FillingProgress(IFillable fillable, long chatId, bool isVerification = false)
    {
        IsVerification = isVerification;
        
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
    
    public static async Task<bool> FinishFilling(long chatId)
    {
        var progress = GetProgress(chatId);
        try
        {
            progress.fillable.FillClass(progress.Answers);
        }
        catch (Exception e)
        {
            await SharedHandlers.SendMessage("Общежития с заданным названием не существует", chatId);
            return false;
        }
        
        ChatId2FillingProgress.Remove(chatId);
        return true;
    }

    public static void CancelFilling(long chatId)
    {
        ChatId2FillingProgress.Remove(chatId);
    }
}