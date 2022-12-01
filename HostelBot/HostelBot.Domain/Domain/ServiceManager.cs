namespace HostelBot.Domain.Domain;

public class ServiceManager
{
    private readonly List<string> serviceNames = new List<string>();

    public void AddService(string name)
    {
        serviceNames.Add(name);
    }

    public List<string> GetServiceNames()
    {
        return new List<string>(serviceNames);
    }
}