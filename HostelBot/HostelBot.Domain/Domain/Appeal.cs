using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Text.Json.Serialization;
using HostelBot.Domain.Infrastructure;

namespace HostelBot.Domain.Domain
{
    public class Appeal : Entity<Appeal, string>, ICanFill
    {
        public new string Id => Name;
        public Appeal() { }
    
        public Appeal(string name, Resident resident)
        {
            Resident = resident;
            Name = name;
        }

        [Key]
        [JsonPropertyName("Name")]
        public string Name { get; }

        [Question("Опишите Вашу проблему", ViewType.TextEnter)]
        [JsonPropertyName("Content")]
        public string Content { get; set; }
        
        [ForeignKey("Resident")]
        public Resident Resident { get; }
        
        public IReadOnlyCollection<PropertyInfo> GetFields() => Properties;
    }
}