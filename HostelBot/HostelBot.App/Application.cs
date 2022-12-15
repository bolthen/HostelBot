using HostelBot.Domain.Domain;
using HostelBot.Domain.Infrastructure;

namespace HostelBot.App;

public class Application : IApplication
{
    private readonly Command[] baseCommands;
    private readonly ResidentRegistrationCommand residentRegistrationCommand;
    
    public Application(Command[] baseCommands, IEnumerable<Manager<Resident>> managers)
    {
        this.baseCommands = baseCommands;
        residentRegistrationCommand = new ResidentRegistrationCommand(managers, baseCommands.ToList());
    }
    
    public IReadOnlyCollection<Command> GetBaseCommands()
    {
        return baseCommands;
    }
    
    public Command GetRegistrationCommand()
    {
        return residentRegistrationCommand;
    }
}