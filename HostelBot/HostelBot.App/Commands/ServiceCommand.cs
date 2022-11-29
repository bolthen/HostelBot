namespace HostelBot.App;

public class ServiceCommand : ICommand
{
    public ServiceCommand(IService service)
    {
        Description = service.Name;
    }
    
    public string Description { get; private set; }

    public IInteractionScenario GetScenario()
    {
        throw new NotImplementedException();
    }
}