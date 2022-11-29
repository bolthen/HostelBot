using System.Reflection;
using System.Text.Json.Serialization;
using HostelBot.Domain.Infrastructure;

namespace HostelBot.Domain.Domain
{
    public class Appeal : Entity<string, Appeal>, ICanFill
    {
        public IReadOnlyCollection<PropertyInfo> GetFields() => Properties;
        
        [QuestionAttribute("Опишите Вашу проблему", ViewType.TextEnter)]
        [JsonPropertyName("Content")]
        public string Content { get; set; }
    }
}