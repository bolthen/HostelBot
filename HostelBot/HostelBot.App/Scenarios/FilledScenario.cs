using System.Text.Json;
using HostelBot.Domain.Infrastructure;

namespace HostelBot.App;

public abstract class FilledScenario<T> : IInteractionScenario 
    where T : ICanFill, new()
{
    public abstract List<ICommand> GetSubcommands();
    public abstract object[] GetStaticInfo();

    public ICanFill GetFillClass() => new T();

    public void HandleFilledClass(string data)
    {
        HandleFilledClass(JsonSerializer.Deserialize<T>(data));
    }
    
    public abstract void HandleFilledClass(T filledObject);
}
