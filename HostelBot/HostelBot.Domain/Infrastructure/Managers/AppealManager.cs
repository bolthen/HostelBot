using HostelBot.Domain.Domain;
using HostelBot.Domain.Infrastructure.Services;

namespace HostelBot.Domain.Infrastructure;

public class AppealManager : Manager<Appeal>
{
    private readonly ResidentService residentService;

    public AppealManager(ResidentService residentService)
    {
        this.residentService = residentService;
    }
    
    protected override void Handle(Appeal value)
    {
        var resident = residentService.GetAsync(value.ResidentId).Result;
        resident.AddAppeal(value);
        residentService.UpdateAsync(resident);
    }
}