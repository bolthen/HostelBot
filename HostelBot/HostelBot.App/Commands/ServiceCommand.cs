namespace HostelBot.App;

public class ServiceCommand : ICommand
{
    public ServiceCommand(IService service)
    {
        Name = service.Name;
    }
    
    public string Name { get; private set; }

    public IInteractionScenario GetScenario()
    {
        throw new NotImplementedException();
    }
}