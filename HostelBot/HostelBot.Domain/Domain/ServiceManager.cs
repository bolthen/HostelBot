namespace HostelBot.Domain.Domain;

public static class ServiceManager
{
    private static readonly List<string> serviceNames = new List<string>();

    public static void AddService(string name)
    {
        serviceNames.Add(name);
    }

    public static List<string> GetServiceNames()
    {
        return new List<string>(serviceNames);
    }
}