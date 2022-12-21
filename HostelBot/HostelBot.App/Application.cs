using HostelBot.Domain.Domain;
using HostelBot.Domain.Infrastructure;
using HostelBot.Domain.Infrastructure.Managers;

namespace HostelBot.App;

public class Application : IApplication
{
    private readonly CheckRegistrationCommand residentRegistrationCommand;
    private readonly EntityChangesHandler<Appeal> appealEntityChangesHandler;
    private readonly EntityChangesHandler<Resident> residentEntityChangesHandler;
    
    public Application(CheckRegistrationCommand residentRegistrationCommand,
        EntityChangesHandler<Appeal> appealEntityChangesHandler, EntityChangesHandler<Resident> residentEntityChangesHandler)
    {
        this.residentRegistrationCommand = residentRegistrationCommand;
        this.appealEntityChangesHandler = appealEntityChangesHandler;
        this.residentEntityChangesHandler = residentEntityChangesHandler;
    }

    public Command GetStartCommand()
    {
        return residentRegistrationCommand;
    }

    public EntityChangesHandler<Appeal> GetAppealChangesManager()
    {
        return appealEntityChangesHandler;
    }
    
    public EntityChangesHandler<Resident> GetResidentChangesManager()
    {
        return residentEntityChangesHandler;
    }
}