using HostelBot.Domain.Domain;
using HostelBot.Domain.Infrastructure.Managers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace HostelBot.Domain.Infrastructure;

public class RepositoryChangesParser
{
    private readonly AppealChangesManager appealChangesManager;
    private readonly ResidentChangesManager residentChangesManager;

    private RepositoryChangesParser(AppealChangesManager appealChangesManager, ResidentChangesManager residentChangesManager)
    {
        this.appealChangesManager = appealChangesManager;
        this.residentChangesManager = residentChangesManager;
    }
    
    private void ParseRepositoryChanges(object sender, EntityEntryEventArgs e)
    {
        switch (e.Entry.Entity)
        {
            case Appeal appeal:
                appealChangesManager.HandleUtilityChanges(appeal);
                break;
            case Resident resident:
                residentChangesManager.HandleResidentChanges(resident);
                break;
        }
    }
}