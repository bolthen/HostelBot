using HostelBot.Domain.Infrastructure;

namespace HostelBot.App;

public interface IInteractionScenario
{
    public List<ICommand> GetSubcommands();

    public object[] GetStaticInfo();

    public IFiller? GetFiller();
}