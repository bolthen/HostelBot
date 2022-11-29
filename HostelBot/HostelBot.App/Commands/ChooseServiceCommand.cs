namespace HostelBot.App;

public class ChooseServiceCommand : ICommand
{
    public string Name => "Услуги";

    public IInteractionScenario GetScenario()
    {
        return new ChooseServiceInteractionScenario();
    }
}