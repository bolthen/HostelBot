using HostelBot.Domain.Infrastructure;
using HostelBot.Domain.Infrastructure.Repository;

namespace HostelBot.Domain.Domain;

public class UtilityFillableManager : Manager<UtilityFillable>
{
    public UtilityFillableManager(ResidentRepository residentRepository) : base(residentRepository)
    {
    }

    protected override async Task Handle(UtilityFillable value)
    {
        var resident = await residentRepository.GetAsync(value.ResidentId);
        var utility = new Utility(value.Name, value.Content, resident);
        resident.AddUtility(utility);
        await residentRepository.UpdateAsync(resident);
    }
}