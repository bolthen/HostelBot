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

    public IFiller GetFiller()
    {
        throw new NotImplementedException();
    }
}