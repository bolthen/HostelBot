using System.ComponentModel.DataAnnotations;

namespace HostelBot.Ui.TelegramBot.Handlers.Filling;

internal class StepByStepFilling
{
    public Dictionary<string, string> Answers { get; } = new();
    private Question[] Questions { get; }
    
    private int Stage { get; set; } = 0;
    private bool IsCompleted => Stage == Questions.Length;

    public StepByStepFilling(Question[] questions)
    {
        Questions = questions;
    }
    
    private void SaveResponse(string response)
    {
        var key = Questions[Stage].Key;
        Answers[key] = response;
    }
    
    private bool TryValidateRegex(string response, out string? errorMessage)
    {
        errorMessage = null;
        
        var regex = Questions[Stage].Regex;
        if (regex is null || regex.IsValid(response))
            return true;

        errorMessage = regex.ErrorMessage;
        return false;
    }

    public string GetNextQuestion()
    {
        return Questions[Stage].Caption;
    }
    
    public (CurrentProgressStatus progressStatus, string? errorMessage) HandleResponse(string text)
    {
        if (!TryValidateRegex(text, out var errorMessage))
            return (CurrentProgressStatus.RegexFailed, errorMessage);
        
        SaveResponse(text);
        Stage++;

        if (IsCompleted)
            return (CurrentProgressStatus.Completed, null);
        
        return (CurrentProgressStatus.WrittenDown, null);
    }

    public class Question
    {
        public Question(string key, string caption, RegularExpressionAttribute? regex = null)
        {
            Key = key;
            Caption = caption;
            Regex = regex;
        }

        public string Key { get; }
        public string Caption { get; }
        public RegularExpressionAttribute? Regex { get; }
    }
    
    public enum CurrentProgressStatus
    {
        WrittenDown, RegexFailed, Completed
    }
}