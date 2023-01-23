using System.ComponentModel;
using HostelBot.Domain.Domain;
using HostelBot.Domain.Infrastructure.Managers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace HostelBot.Domain.Infrastructure;

public class RepositoryChangesParser
{
    private readonly EntityChangesHandler<Appeal> appealEntityChangesHandler;
    private readonly EntityChangesHandler<Resident> residentEntityChangesHandler;

    public RepositoryChangesParser(EntityChangesHandler<Appeal> appealEntityChangesHandler, EntityChangesHandler<Resident> residentEntityChangesHandler)
    {
        this.appealEntityChangesHandler = appealEntityChangesHandler;
        this.residentEntityChangesHandler = residentEntityChangesHandler;
    }
    
    public void ParseRepositoryChanges(object? sender, PropertyChangedEventArgs entityTrackedEventArgs)
    {
        var propertyName = entityTrackedEventArgs.PropertyName;
        if (sender is Resident {IsAccepted: true} resident && propertyName == nameof(resident.IsAccepted))
        {
            residentEntityChangesHandler.OnHandleChanges(resident);
        }
        if (sender is Appeal appeal && appeal.Answer != null && propertyName == nameof(appeal.Answer))
        {
            appealEntityChangesHandler.OnHandleChanges(appeal);
        }
    }
}