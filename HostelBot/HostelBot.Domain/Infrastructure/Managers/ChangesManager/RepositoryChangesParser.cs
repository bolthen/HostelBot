using HostelBot.Domain.Domain;
using HostelBot.Domain.Infrastructure.Managers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace HostelBot.Domain.Infrastructure;

public class RepositoryChangesParser
{
    private readonly ChangesManager<Appeal> appealChangesManager;
    private readonly ChangesManager<Resident> residentChangesManager;

    public RepositoryChangesParser(ChangesManager<Appeal> appealChangesManager, ChangesManager<Resident> residentChangesManager)
    {
        this.appealChangesManager = appealChangesManager;
        this.residentChangesManager = residentChangesManager;
    }
    
    public void ParseRepositoryChanges(object? sender, DetectedEntityChangesEventArgs detectedEntityChangesEventArgs)
    {
        switch (detectedEntityChangesEventArgs.Entry.Entity)
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