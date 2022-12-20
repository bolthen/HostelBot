using HostelBot.Domain.Infrastructure;
using HostelBot.Domain.Infrastructure.Repository;

namespace HostelBot.Domain.Domain;

public class FillableUtilityManager : Manager<UtilityFillable>
{
    private readonly ResidentRepository residentRepository;
    
    public FillableUtilityManager(ResidentRepository residentRepository)
    {
        this.residentRepository = residentRepository;
    }

    protected override void Handle(UtilityFillable value)
    {
        var utility = new Utility(value.Name, value.Content);
        var resident = residentRepository.GetAsync(value.ResidentId).Result;
        resident.AddUtility(utility);
        residentRepository.UpdateAsync(resident);
    }
}