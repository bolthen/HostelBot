using HostelBot.Domain.Infrastructure;
using HostelBot.Domain.Infrastructure.Services;

namespace HostelBot.Domain.Domain;

public class UtilityManager : Manager<Utility>
{
    private UtilityService utilityService;
    public UtilityManager(UtilityService utilityService)
    {
        this.utilityService = utilityService;
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
        utilityService.CreateAsync(value);
        return;
    }
}