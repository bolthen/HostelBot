using HostelBot.Domain.Domain;
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

    public ICanFill GetFillClass() => new Appeal();

    public void HandleFilledClass()
    {
        throw new NotImplementedException();
    }
}