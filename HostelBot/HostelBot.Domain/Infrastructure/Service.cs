using System.Reflection;
using HostelBot.App;

namespace HostelBot.Domain.Infrastructure;

public class Service : Entity<Service, int>, IService
{
    public string Name { get; set; }
    
    public Service()
    {
    }
    
    public Service(string name)
    {
        Name = name;
    }

    public IReadOnlyCollection<PropertyInfo> GetFields()
    {
        throw new NotImplementedException();
    }
}