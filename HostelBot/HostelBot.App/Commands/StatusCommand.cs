namespace HostelBot.App;

public class StatusCommand : ICommand
{
    public string Description => "Статус";
    public IInteractionScenario GetScenario()
    {
        throw new NotImplementedException();
    }
}