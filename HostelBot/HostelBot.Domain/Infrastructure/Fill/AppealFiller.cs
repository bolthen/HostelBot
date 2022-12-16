using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text.Json;
using System.Xml.Serialization;
using HostelBot.Domain.Domain;

namespace HostelBot.Domain.Infrastructure;

public class AppealFiller : Filler<AppealFiller>, IFillable
{
    public override void OnFilled()
    {
        foreach (var observer in observers.ToArray())
            observer.OnCompleted(this);
    }

    public IReadOnlyCollection<PropertyInfo> GetFields() => Properties;

    public long ResidentId { get; set; }
    
    [Question("Как вас зовут", ViewType.TextEnter)]
    [RegularExpression(@"^([А-ЩЭ-Я][а-я]+-?)+$",
        ErrorMessage = "Имя должно начинаться с заглавной буквы, не иметь пробелов")]
    public string Name { get; set; }

    [Question("Опишите Вашу проблему", ViewType.TextEnter)]
    public string Content { get; set; }
}