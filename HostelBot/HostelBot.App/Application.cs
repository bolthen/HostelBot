using HostelBot.Domain.Domain;
using HostelBot.Domain.Infrastructure;

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
    
    public Command GetRegistrationCommands(string name, IEnumerable<Manager<Resident>> managers)
    {
        return new ResidentRegistrationCommand(name, managers);
    }
}