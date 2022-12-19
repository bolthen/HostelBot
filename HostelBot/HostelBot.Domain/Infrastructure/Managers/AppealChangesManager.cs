using HostelBot.Domain.Domain;

namespace HostelBot.Domain.Infrastructure.Managers;

public class AppealChangesManager
{
    private readonly Action<Appeal> appealHandler;
    private event Action<Appeal> HandleChahges; 

    public AppealChangesManager(Action<Appeal> appealHandler)
    {
        this.appealHandler = appealHandler;
        HandleChahges += appealHandler;
    }

    public void HandleUtilityChanges(Appeal appeal)
    {
        HandleChahges(appeal);
    }
}