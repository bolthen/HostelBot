using System.ComponentModel.DataAnnotations;
using System.Reflection;
using HostelBot.Domain.Infrastructure;

namespace HostelBot.Ui.TelegramBot.Handlers.Filling;
 
internal class FillingProgress
{
    public readonly IFillable fillable;
    public bool IsRegistration { get; }

    private readonly StepByStepFilling stepByStepFilling;

    public FillingProgress(IFillable fillable, long chatId, bool isRegistration = false)
    {
        IsRegistration = isRegistration;

        this.fillable = fillable;

        var questions = fillable
            .GetFields()
            .Where(propertyInfo => propertyInfo.GetCustomAttribute<QuestionAttribute>() != null)
            .Select(property =>
                new StepByStepFilling.Question(property.Name,
                    property.GetCustomAttribute<QuestionAttribute>()!.Question,
                    property.GetCustomAttribute<RegularExpressionAttribute>())
            )
            .ToArray();

        stepByStepFilling = new StepByStepFilling(questions);

        ChatId2FillingProgress[chatId] = this;
    }

    public string GetNextQuestion()
    {
        return stepByStepFilling.GetNextQuestion();
    }

    public (StepByStepFilling.CurrentProgressStatus progressStatus, string? errorMessage) HandleResponse(string text)
    {
        return stepByStepFilling.HandleResponse(text);
    }

    public Dictionary<string, string> GetAnswers()
    {
        return stepByStepFilling.Answers;
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
}