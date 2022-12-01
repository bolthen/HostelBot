using System.Reflection;
using System.Text.Json.Serialization;
using HostelBot.Domain.Infrastructure;

namespace HostelBot.Domain;

public class Service : Entity<Service, int>, IService
{
    public Service()
    {
    }
    
    public Service(string name)
    {
        Name = name;
    }
    
    public string Name { get; private set; }
    
    [Question("Опишите Вашу проблему", ViewType.TextEnter)]
    [JsonPropertyName("Content")]
    public string Content { get; set; }

    public bool Filled { get; set; }
    public IReadOnlyCollection<PropertyInfo> GetFields() => Properties;
}