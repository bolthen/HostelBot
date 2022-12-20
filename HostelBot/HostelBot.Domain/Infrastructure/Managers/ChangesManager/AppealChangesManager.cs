using HostelBot.Domain.Domain;

namespace HostelBot.Domain.Infrastructure.Managers;

public class AppealChangesManager : ChangesManager<Appeal>
{
    public override void OnHandleChanges(Appeal appeal)
    {
        if (appeal.Answer != null)
            base.OnHandleChanges(appeal);
    }
}