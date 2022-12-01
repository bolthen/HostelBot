using HostelBot.Domain;
using HostelBot.Domain.Domain;
using HostelBot.Domain.Infrastructure;

namespace HostelBot.App;

public class ChooseServiceCommand : Command
{
    private readonly ServiceManager serviceManager;
    
    public ChooseServiceCommand(ServiceManager serviceManager): base("Услуги")
    {
        this.serviceManager = serviceManager;
    }
    
    public override List<Command> GetSubcommands()
    {
        return serviceManager
            .GetServiceNames()
            .Select(name => new ServiceCommand(new Service("Клининг")))
            .Cast<Command>()
            .ToList();
    }
}