using HostelBot.Domain.Domain;
using HostelBot.Domain.Infrastructure.Managers;

namespace HostelBot.App;

public interface IApplication
{
    Command GetStartCommand();
    public ChangesManager<Appeal> GetAppealChangesManager();
    public ChangesManager<Resident> GetResidentChangesManager();
}