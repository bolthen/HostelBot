using HostelBot.Domain.Domain;
using HostelBot.Domain.Infrastructure.Repository;

namespace HostelBot.Domain.Infrastructure;

public class FillableAppealManager : Manager<AppealFillable>
{
    private readonly ResidentRepository residentRepository;

    public FillableAppealManager(ResidentRepository residentRepository)
    {
        this.residentRepository = residentRepository;
    }
    
    protected override void Handle(AppealFillable value)
    {
        var resident = residentRepository.GetAsync(value.ResidentId).Result;
        var appeal = new Appeal(resident, value.Content);
        resident.AddAppeal(appeal);
        residentRepository.UpdateAsync(resident);
    }
}