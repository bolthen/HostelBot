using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text.Json;
using System.Xml.Serialization;
using HostelBot.Domain.Domain;

namespace HostelBot.Domain.Infrastructure;

public class AppealFillable : Fillable<AppealFillable>, IFillable
{
    public override void OnFilled()
    {
        foreach (var observer in observers.ToArray())
            observer.OnCompleted(this);
    }

    public long ResidentId { get; set; }
    
    [Question("Напишите ваше обращение", ViewType.TextEnter)]
    public string Content { get; set; }
}