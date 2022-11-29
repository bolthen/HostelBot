namespace HostelBot.App;

public interface IApplication
{
    IReadOnlyCollection<ICommand> GetBaseCommands();
}