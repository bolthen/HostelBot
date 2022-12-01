using HostelBot.Domain.Infrastructure;

namespace HostelBot.App;

public class StatusInteractionScenario : FilledScenario<StatusFiller>
{
    public override List<ICommand> GetSubcommands()
    {
        throw new NotImplementedException();
    }

    public override object[] GetStaticInfo()
    {
        throw new NotImplementedException();
    }
}