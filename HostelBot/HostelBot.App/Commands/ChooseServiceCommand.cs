namespace HostelBot.App;

public class ChooseServiceCommand : ICommand
{
    public string Description => "Услуги";

    public IInteractionScenario GetScenario()
    {
        return new ChooseServiceInteractionScenario();
    }
}