namespace HostelBot.App;

public interface IApplication
{
    IReadOnlyCollection<Command> GetBaseCommands();
}