using HostelBot.Domain.Domain;
using HostelBot.Domain.Infrastructure.Services;

namespace HostelBot.Domain.Infrastructure;

public class AppealManager : Manager<AppealFiller>
{
    private readonly ResidentRepository residentService;

    public AppealManager(ResidentRepository residentService)
    {
        this.residentService = residentService;
    }
    
    protected override void Handle(AppealFiller value)
    {
        var resident = residentService.GetAsync(value.ResidentId).Result;
        var appeal = new Appeal(value.Name, resident, value.Content);
        resident.AddAppeal(appeal);
        residentService.UpdateAsync(resident);
    }
}