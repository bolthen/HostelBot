using System.Windows.Input;
using HostelBot.Domain;
using HostelBot.Domain.Domain;
using HostelBot.Domain.Infrastructure;

namespace HostelBot.App;

public class ChooseServiceInteractionScenario : IInteractionScenario
{
    public List<ICommand> GetSubcommands()
    {
        return ServiceManager
            .GetServiceNames()
            .Select(name => new ServiceCommand(new Service(name)))
            .Cast<ICommand>()
            .ToList();
    }

    public object[] GetStaticInfo()
    {
        return Array.Empty<object>();
    }

    public IFiller? GetFiller()
    {
        return null;
    }
}