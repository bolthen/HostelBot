using HostelBot.Domain.Domain;
using HostelBot.Domain.Infrastructure.Managers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace HostelBot.Domain.Infrastructure;

public class RepositoryChangesParser
{
    private readonly AppealChangesManager appealChangesManager;
    private readonly ResidentChangesManager residentChangesManager;

    public RepositoryChangesParser(AppealChangesManager appealChangesManager, ResidentChangesManager residentChangesManager)
    {
        this.appealChangesManager = appealChangesManager;
        this.residentChangesManager = residentChangesManager;
    }
    
    public void ParseRepositoryChanges(object? sender, EntityTrackedEventArgs entityTrackedEventArgs)
    {
        switch (entityTrackedEventArgs.Entry.Entity)
        {
            case Appeal appeal:
                appealChangesManager.OnHandleChanges(appeal);
                break;
            case Resident resident:
                residentChangesManager.OnHandleChanges(resident);
                break;
        }
    }
}