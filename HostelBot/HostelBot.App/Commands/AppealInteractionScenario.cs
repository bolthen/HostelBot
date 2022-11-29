using HostelBot.Domain.Infrastructure;

namespace HostelBot.App;

public class AppealInteractionScenario : IInteractionScenario
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

    public void HandleFilledClass()
    {
        throw new NotImplementedException();
    }
}