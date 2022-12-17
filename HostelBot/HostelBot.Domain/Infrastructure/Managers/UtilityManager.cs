using HostelBot.Domain.Infrastructure;
using HostelBot.Domain.Infrastructure.Services;

namespace HostelBot.Domain.Domain;

public class UtilityManager : Manager<UtilityFiller>
{
    private readonly ResidentRepository residentRepository;
    
    public UtilityManager(ResidentRepository residentRepository)
    {
        this.residentRepository = residentRepository;
    }

    protected override void Handle(UtilityFiller value)
    {
        var utility = new Utility(value.Name, value.Content);
        var resident = residentRepository.GetAsync(value.ResidentId).Result;
        resident.AddUtility(utility);
        residentRepository.UpdateAsync(resident);
    }
}