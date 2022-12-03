using HostelBot.Domain.Infrastructure;

namespace HostelBot.Domain.Domain;

public class ServiceManager : Manager<Service>
{
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

    protected override void Handle(Service value)
    {
        return;
    }
}