using HostelBot.Domain.Domain;
using HostelBot.Domain.Infrastructure;

namespace HostelBot.App;

public class AppealInteractionScenario : FilledScenario<AppealFiller>
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