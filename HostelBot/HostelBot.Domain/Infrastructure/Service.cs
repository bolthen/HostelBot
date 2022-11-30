namespace HostelBot.Domain.Infrastructure;

public class Service : Entity<Service, int>, IService
{
    public string Name { get; }
    
    public Service(string name)
    {
        Name = name;
    }
}