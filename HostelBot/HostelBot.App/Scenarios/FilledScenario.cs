using System.Text.Json;
using HostelBot.Domain.Infrastructure;

namespace HostelBot.App;

public abstract class FilledScenario<TFiller> : IInteractionScenario 
    where TFiller : IFiller, new()
{
    public abstract List<ICommand> GetSubcommands();
    public abstract object[] GetStaticInfo();

    public IFiller GetFiller()
    {
        return new TFiller();
    }
}
