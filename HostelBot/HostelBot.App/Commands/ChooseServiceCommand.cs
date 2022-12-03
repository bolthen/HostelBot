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
        var names = new List<string> {"Клининг", "Сантехник", "Электрик"};
        var commands = new List<Command>();
        
        foreach (var name in names)
        {
            var observer = new ServiceManager();
            var service = new Service(name);
            observer.Subscribe(service);
            commands.Add(new ServiceCommand(service));
        }

        return commands;
        
        return serviceManager
            .GetServiceNames()
            .Select(name => new ServiceCommand(new Service(name)))
            .Cast<Command>()
            .ToList();
    }
}