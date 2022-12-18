using HostelBot.Domain.Domain;
using HostelBot.Domain.Infrastructure;

namespace HostelBot.App;

public class Application : IApplication
{
    private readonly CheckRegistrationCommand residentRegistrationCommand;
    
    public Application(CheckRegistrationCommand residentRegistrationCommand)
    {
        this.residentRegistrationCommand = residentRegistrationCommand;
    }

    public Command GetStartCommand()
    {
        return residentRegistrationCommand;
    }
}