using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Text.Json.Serialization;
using HostelBot.Domain.Infrastructure;

namespace HostelBot.Domain.Domain
{
    public class Appeal : Entity<Appeal>, ICanFill
    {
        public Appeal() { }
    
        public Appeal(string name, Resident resident)
        {
            Resident = resident;
            Name = name;
        }
        
        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [Question("Опишите Вашу проблему", ViewType.TextEnter)]
        [JsonPropertyName("Content")]
        public string Content { get; set; }
        
        public Resident Resident { get; set;}
        
        public IReadOnlyCollection<PropertyInfo> GetFields() => Properties;
    }
}