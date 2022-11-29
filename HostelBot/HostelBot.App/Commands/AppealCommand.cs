namespace HostelBot.App;

public class AppealCommand : ICommand
{
    public string Description => "Обращение";
    public IInteractionScenario GetScenario()
    {
        throw new NotImplementedException();
    }
}