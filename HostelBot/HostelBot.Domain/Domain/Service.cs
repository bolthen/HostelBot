using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text.Json.Serialization;
using HostelBot.Domain.Infrastructure;

namespace HostelBot.Domain;

public class Service : Entity<Service, string>, IService
{
    public new string Id => Name;
    public Service() { }
    
    public Service(string name)
    {
        Name = name;
    }
    
    [Key]
    [JsonPropertyName("Name")]
    public string Name { get; }
    
    [Question("Опишите Вашу проблему", ViewType.TextEnter)]
    [JsonPropertyName("Content")]
    public string Content { get; set; }

    public IReadOnlyCollection<PropertyInfo> GetFields() => Properties;
}