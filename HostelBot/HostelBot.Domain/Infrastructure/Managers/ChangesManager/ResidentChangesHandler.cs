using HostelBot.Domain.Domain;

namespace HostelBot.Domain.Infrastructure.Managers;

public class ResidentChangesHandler : EntityChangesHandler<Resident>
{
    public override void OnHandleChanges(Resident resident)
    {
        if (resident.IsAccepted)
            base.OnHandleChanges(resident);
    }
}