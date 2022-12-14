using HostelBot.Domain.Domain;
using HostelBot.Domain.Infrastructure.Services;

namespace HostelBot.Domain.Infrastructure.Managers;

public class ResidentManager : Manager<Resident>
{
    private readonly ResidentRepository residentRepository;

    public ResidentManager(ResidentRepository residentRepository)
    {
        this.residentRepository = residentRepository;
    }
    
    protected override void Handle(Resident value)
    {
        residentRepository.CreateAsync(value);
    }
}