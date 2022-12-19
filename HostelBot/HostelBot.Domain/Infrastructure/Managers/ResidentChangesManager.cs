using HostelBot.Domain.Domain;

namespace HostelBot.Domain.Infrastructure.Managers;

public class ResidentChangesManager
{
    private readonly Action<Resident> residentHandler;
    private event Action<Resident> HandleChahges; 

    public ResidentChangesManager(Action<Resident> residentHandler)
    {
        this.residentHandler = residentHandler;
        HandleChahges += residentHandler;
    }

    public void HandleResidentChanges(Resident appeal)
    {
        HandleChahges(appeal);
    }
}