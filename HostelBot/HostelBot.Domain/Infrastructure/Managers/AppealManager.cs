using HostelBot.Domain.Domain;
using HostelBot.Domain.Infrastructure.Services;

namespace HostelBot.Domain.Infrastructure;

public class AppealManager : Manager<AppealFiller>
{
    private readonly ResidentRepository residentRepository;

    public AppealManager(ResidentRepository residentRepository)
    {
        this.residentRepository = residentRepository;
    }
    
    protected override void Handle(AppealFiller value)
    {
        var resident = residentRepository.GetAsync(value.ResidentId).Result;
        var appeal = new Appeal(value.Name, resident, value.Content);
        resident.AddAppeal(appeal);
        residentRepository.UpdateAsync(resident);
    }
}