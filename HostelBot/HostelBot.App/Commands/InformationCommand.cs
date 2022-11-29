namespace HostelBot.App;

public class InformationCommand : ICommand
{
    public string Description => "Информация";
    public IInteractionScenario GetScenario()
    {
        return new InformationInteractionScenario();
    }
}