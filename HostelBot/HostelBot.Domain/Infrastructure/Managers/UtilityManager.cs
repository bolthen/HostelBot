using HostelBot.Domain.Infrastructure;
using HostelBot.Domain.Infrastructure.Services;

namespace HostelBot.Domain.Domain;

public class UtilityManager : Manager<Utility>
{
    private readonly UtilityService utilityService;
    private readonly ResidentService residentService;

    public UtilityManager(UtilityService utilityService, ResidentService residentService)
    {
        this.utilityService = utilityService;
        this.residentService = residentService;
    }

    private readonly List<string> serviceNames = new();

    public void AddService(string name)
    {
        serviceNames.Add(name);
    }

    public IReadOnlyList<string> GetServiceNames()
    {
        return new List<string> {"Клининг", "Сантехник", "Электрик"};
        //return new List<string>(serviceNames);
    }

    protected override void Handle(Utility value)
    {
        var resident = residentService.GetAsync(value.ResidentId).Result;
        resident.AddUtility(value);
        residentService.UpdateAsync(resident);
    }
}