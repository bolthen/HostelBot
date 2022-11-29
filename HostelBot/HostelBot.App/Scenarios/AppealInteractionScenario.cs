using HostelBot.Domain.Domain;
using HostelBot.Domain.Infrastructure;

namespace HostelBot.App;

public class AppealInteractionScenario : FilledScenario<Appeal>
{
    public override List<ICommand> GetSubcommands() => new();
    public override object[] GetStaticInfo() => Array.Empty<object>();

    public override void HandleFilledClass(Appeal filledObject)
    {
        throw new NotImplementedException();
    }
}