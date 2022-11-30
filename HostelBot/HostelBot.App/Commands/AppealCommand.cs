namespace HostelBot.App;

public class AppealCommand : ICommand
{
    public string Name => "Обращение";
    
    public IInteractionScenario GetScenario()
    {
        return new AppealInteractionScenario();
    }
}