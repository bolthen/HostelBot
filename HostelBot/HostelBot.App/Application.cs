namespace HostelBot.App;

public class Application : IApplication
{
    private readonly Command[] baseCommands;
    
    public Application(Command[] baseCommands)
    {
        this.baseCommands = baseCommands;
    }
    
    public IReadOnlyCollection<Command> GetBaseCommands()
    {
        return baseCommands;
    }
}