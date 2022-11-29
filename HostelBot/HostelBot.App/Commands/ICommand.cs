namespace HostelBot.App;

public interface ICommand
{
    string Name { get; }
    IInteractionScenario GetScenario();
}