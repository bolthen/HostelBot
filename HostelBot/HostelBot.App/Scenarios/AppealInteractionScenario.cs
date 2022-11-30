using HostelBot.Domain.Domain;
using HostelBot.Domain.Infrastructure;

namespace HostelBot.App;

public class AppealInteractionScenario : FilledScenario<AppealFiller>
{
    public override List<ICommand> GetSubcommands() => new();

    public override object[] GetStaticInfo()
    {
        throw new NotImplementedException();
    }
}