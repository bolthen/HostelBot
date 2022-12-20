using HostelBot.Domain.Domain;

namespace HostelBot.Domain.Infrastructure.Managers;

public class ResidentChangesManager : ChangesManager<Resident>
{
    public override void OnHandleChanges(Resident resident)
    {
        if (resident.Accepted)
            base.OnHandleChanges(resident);
    }
}