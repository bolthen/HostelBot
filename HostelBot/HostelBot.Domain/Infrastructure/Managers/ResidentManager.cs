using HostelBot.Domain.Domain;
using HostelBot.Domain.Infrastructure.Services;

namespace HostelBot.Domain.Infrastructure.Managers;

public class ResidentManager : Manager<Resident>
{
    private readonly ResidentRepository residentService;

    public ResidentManager(ResidentRepository residentService)
    {
        this.residentService = residentService;
    }
    
    protected override void Handle(Resident value)
    {
        residentService.CreateAsync(value);
    }
}