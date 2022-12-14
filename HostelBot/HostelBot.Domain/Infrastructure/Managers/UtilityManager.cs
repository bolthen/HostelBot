using HostelBot.Domain.Infrastructure;
using HostelBot.Domain.Infrastructure.Services;

namespace HostelBot.Domain.Domain;

public class UtilityManager : Manager<Utility>
{
    private readonly ResidentRepository residentRepository;

    
    public UtilityManager(ResidentRepository residentRepository)
    {
        this.residentRepository = residentRepository;
    }
    
    public IReadOnlyList<string> GetServiceNames()
    {
        return new List<string> {"Клининг", "Сантехник", "Электрик"};
        //return new List<string>(serviceNames);
    }

    protected override void Handle(Utility value)
    {
        var resident = residentRepository.GetAsync(value.ResidentId).Result;
        resident.AddUtility(value);
        residentRepository.UpdateAsync(resident);
    }
}