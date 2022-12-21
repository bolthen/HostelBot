using HostelBot.Domain.Domain;
using HostelBot.Domain.Infrastructure.Managers;

namespace HostelBot.App;

public interface IApplication
{
    Command GetStartCommand();
    public EntityChangesHandler<Appeal> GetAppealChangesManager();
    public EntityChangesHandler<Resident> GetResidentChangesManager();
}