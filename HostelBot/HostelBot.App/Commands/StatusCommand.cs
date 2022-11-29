namespace HostelBot.App;

public class StatusCommand : ICommand
{
    public string Name => "Статус";
    public IInteractionScenario GetScenario()
    {
        throw new NotImplementedException();
    }
}