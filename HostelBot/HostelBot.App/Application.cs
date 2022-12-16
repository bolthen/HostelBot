using HostelBot.Domain.Domain;
using HostelBot.Domain.Infrastructure;

namespace HostelBot.App;

public class Application : IApplication
{
    private readonly Command[] baseCommands;
    private readonly ResidentRegistrationCommand residentRegistrationCommand;
    
    public Application(Command[] baseCommands, ResidentRegistrationCommand residentRegistrationCommand)
    {
        this.baseCommands = baseCommands;
        this.residentRegistrationCommand = residentRegistrationCommand;
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