using HostelBot.Domain.Domain;
using HostelBot.Domain.Infrastructure;
using HostelBot.Domain.Infrastructure.Managers;

namespace HostelBot.App;

public class Application : IApplication
{
    private readonly CheckRegistrationCommand residentRegistrationCommand;
    private readonly ChangesManager<Appeal> appealChangesManager;
    private readonly ChangesManager<Resident> residentChangesManager;
    
    public Application(CheckRegistrationCommand residentRegistrationCommand,
        ChangesManager<Appeal> appealChangesManager, ChangesManager<Resident> residentChangesManager)
    {
        this.residentRegistrationCommand = residentRegistrationCommand;
        this.appealChangesManager = appealChangesManager;
        this.residentChangesManager = residentChangesManager;
    }

    public Command GetStartCommand()
    {
        return residentRegistrationCommand;
    }

    public ChangesManager<Appeal> GetAppealChangesManager()
    {
        return appealChangesManager;
    }
    
    public ChangesManager<Resident> GetResidentChangesManager()
    {
        return residentChangesManager;
    }
}