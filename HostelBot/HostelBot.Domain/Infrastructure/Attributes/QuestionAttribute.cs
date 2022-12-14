namespace HostelBot.Domain.Infrastructure;

public class QuestionAttribute : Attribute
{
    public readonly ViewType Type;
    public readonly string Question;
    
    public QuestionAttribute(string question, ViewType viewType)
    {
        Question = question;
        Type = viewType;
    }
}