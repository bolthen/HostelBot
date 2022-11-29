using System.Text.Json;
using HostelBot.Domain.Domain;
using HostelBot.Domain.Infrastructure;

namespace HostelBot.App;

public interface IInteractionScenario
{
    public List<ICommand> GetSubcommands();

    public object[] GetStaticInfo();

    public ICanFill? GetFillClass();

    public void HandleFilledClass(string data);
}

internal abstract class FilledScenario<T> : IInteractionScenario 
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

internal class AppealScenario : FilledScenario<Appeal>
{
    public override List<ICommand> GetSubcommands()
    {
        throw new NotImplementedException();
    }

    public override object[] GetStaticInfo()
    {
        throw new NotImplementedException();
    }

    public override void HandleFilledClass(Appeal filledObject)
    {
        throw new NotImplementedException();
    }
}