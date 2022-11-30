namespace HostelBot.App;

public class ServiceCommand : ICommand
{
    public string Name => service.Name;
    private readonly IService service;
    
    public ServiceCommand(IService service)
    {
        this.service = service;
    }

    public IInteractionScenario GetScenario()
    {
        return new ServiceInteractionScenario();
    }
}