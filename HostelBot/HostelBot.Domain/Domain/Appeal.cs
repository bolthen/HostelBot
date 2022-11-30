using System.Reflection;
using System.Text.Json.Serialization;
using HostelBot.Domain.Infrastructure;

namespace HostelBot.Domain.Domain
{
    public class Appeal : Entity<Appeal, string>, ICanFill
    {
        public IReadOnlyCollection<PropertyInfo> GetFields() => Properties;
        
        [Question("Опишите Вашу проблему", ViewType.TextEnter)]
        [JsonPropertyName("Content")]
        public string Content { get; set; }
    }
}