using HostelBot.Domain.Infrastructure;
using HostelBot.Domain.Infrastructure.Services;

namespace HostelBot.Domain.Domain;

public class UtilityManager : Manager<Utility>
{
    private readonly ResidentService residentService;

    public UtilityManager(ResidentService residentService)
    {
        this.residentService = residentService;
    }
    
    public IReadOnlyList<string> GetServiceNames()
    {
        return new List<string> {"Клининг", "Сантехник", "Электрик"};
    }

    protected override void Handle(Utility value)
    {
        var resident = residentService.GetAsync(value.ResidentId).Result;
        resident.AddUtility(value);
        residentService.UpdateAsync(resident);
    }
}