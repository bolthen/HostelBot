namespace HostelBot.App;

public interface ICommand
{
    string Description { get; }
    IInteractionScenario GetScenario();
}