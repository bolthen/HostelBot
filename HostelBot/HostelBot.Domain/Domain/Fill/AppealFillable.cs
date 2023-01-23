using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text.Json;
using System.Xml.Serialization;
using HostelBot.Domain.Domain;

namespace HostelBot.Domain.Infrastructure;

public class AppealFillable : Fillable<AppealFillable>
{
    public override async Task OnFilled()
    {
        foreach (var observer in observers.ToArray())
            await observer.OnCompleted(this);
    }

    [Question("Напишите ваше обращение к заведующей", ViewType.TextEnter)]
    public string Content { get; set; }
}