using System.Reflection;

namespace HostelBot.Domain.Infrastructure;

public class UtilityFillable : Fillable<UtilityFillable>
{
    public UtilityFillable() {}
    
    public UtilityFillable(string name) => Name = name;
    
    public override void OnFilled()
    {
        foreach (var observer in observers.ToArray())
            observer.OnCompleted(this);
    }
    
    public string Name { get; set; }
    
    [Question("Опишите Вашу проблему", ViewType.TextEnter)]
    public string Content { get; set; }
}