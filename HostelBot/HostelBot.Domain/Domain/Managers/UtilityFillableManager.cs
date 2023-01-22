using HostelBot.Domain.Infrastructure;
using HostelBot.Domain.Infrastructure.Repository;

namespace HostelBot.Domain.Domain;

public class UtilityFillableManager : Manager<UtilityFillable>
{
    public UtilityFillableManager(ResidentRepository residentRepository) : base(residentRepository)
    {
    }

    protected override void Handle(UtilityFillable value)
    {
        var resident = residentRepository.GetAsync(value.ResidentId).Result;
        var utility = new Utility(value.Name, value.Content, resident);
        resident.AddUtility(utility);
        residentRepository.UpdateAsync(resident);
    }
}