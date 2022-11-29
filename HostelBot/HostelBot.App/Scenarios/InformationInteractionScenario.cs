using HostelBot.Domain.Infrastructure;

namespace HostelBot.App;

public class InformationInteractionScenario : IInteractionScenario
{
    public List<ICommand> GetSubcommands()
    {
        throw new NotImplementedException();
    }

    public object[] GetStaticInfo()
    {
        throw new NotImplementedException();
    }

    public ICanFill? GetFillClass()
    {
        throw new NotImplementedException();
    }

    public void HandleFilledClass(string data)
    {
        throw new NotImplementedException();
    }
}