namespace HostelBot.App;

public class InformationCommand : ICommand
{
    public string Name => "Информация";
    public IInteractionScenario GetScenario()
    {
        return new InformationInteractionScenario();
    }
}