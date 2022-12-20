using HostelBot.Domain.Domain;
using HostelBot.Domain.Infrastructure;
using HostelBot.Domain.Infrastructure.Managers;

namespace HostelBot.App;

public class Application : IApplication
{
    private readonly CheckRegistrationCommand residentRegistrationCommand;
    
    public Application(CheckRegistrationCommand residentRegistrationCommand,
        AppealChangesManager appealChangesManager, ResidentChangesManager residentChangesManager)
    {
        this.residentRegistrationCommand = residentRegistrationCommand;
        appealChangesManager.AddChangesHandler(Console.WriteLine);
        residentChangesManager.AddChangesHandler(Console.WriteLine);
    }

    public Command GetStartCommand()
    {
        return residentRegistrationCommand;
    }
}