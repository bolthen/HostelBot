using System.Reflection;

namespace HostelBot.Domain.Infrastructure;

public class UtilityFiller : Filler<UtilityFiller>
{
    public UtilityFiller() {}
    public UtilityFiller(string name) => Name = name;
    
    public override void OnFilled()
    {
        foreach (var observer in observers.ToArray())
            observer.OnCompleted(this);
    }

    public IReadOnlyCollection<PropertyInfo> GetFields() => Properties;

    public string Name { get; set; }
    
    [Question("Опишите Вашу проблему", ViewType.TextEnter)]
    public string Content { get; set; }
}