using HostelBot.Domain.Domain;
using HostelBot.Domain.Infrastructure;
using HostelBot.Domain.Infrastructure.Managers;

namespace HostelBot.App;

public class Application : IApplication
{
    private readonly CheckRegistrationCommand residentRegistrationCommand;
    private readonly EntityChangesHandler<Appeal> appealChangesHandler;
    private readonly EntityChangesHandler<Resident> residentChangesHandler;
    
    public Application(CheckRegistrationCommand residentRegistrationCommand,
        EntityChangesHandler<Appeal> appealEntityChangesHandler, EntityChangesHandler<Resident> residentEntityChangesHandler)
    {
        this.residentRegistrationCommand = residentRegistrationCommand;
        this.appealChangesHandler = appealEntityChangesHandler;
        this.residentChangesHandler = residentEntityChangesHandler;
    }

    public Command GetStartCommand()
    {
        return residentRegistrationCommand;
    }

    public EntityChangesHandler<Appeal> GetAppealChangesHandler()
    {
        return appealChangesHandler;
    }
    
    public EntityChangesHandler<Resident> GetResidentChangesHandler()
    {
        return residentChangesHandler;
    }
}