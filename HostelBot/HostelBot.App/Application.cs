namespace HostelBot.App;

public class Application : IApplication
{
    private readonly ICommand[] baseCommands;
    
    public Application(ICommand[] baseCommands)
    {
        this.baseCommands = baseCommands;
    }
    
    public IReadOnlyCollection<ICommand> GetBaseCommands()
    {
        return baseCommands;
    }
}