namespace HostelBot.App;

public class ServiceCommand : ICommand
{
    public string Name => "Услуги";
    
    public ServiceCommand(/*IService service*/)
    {
        //Name = service.Name;
    }

    public IInteractionScenario GetScenario()
    {
        throw new NotImplementedException();
    }
}