using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text.Json.Serialization;
using HostelBot.Domain.Infrastructure;

namespace HostelBot.Domain.Domain;

public class Service : Entity<Service>, IService
{
    public Service() { }
    
    public Service(string name)
    {
        Name = name;
    }
    
    [JsonPropertyName("Name")]
    public string Name { get; set;}
    
    [Question("Опишите Вашу проблему", ViewType.TextEnter)]
    [JsonPropertyName("Content")]
    public string Content { get; set; }

    public IReadOnlyCollection<PropertyInfo> GetFields() => Properties;
}