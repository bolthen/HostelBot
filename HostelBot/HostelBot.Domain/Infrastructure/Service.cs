namespace HostelBot.Domain.Infrastructure;

public class Service : Entity<Service, int>, IService
{
    public string Name { get; private set; }
    
    public Service(string name)
    {
        Name = name;
    }
}