using System.Reflection;
using System.Text.Json.Serialization;
using HostelBot.Domain.Infrastructure;

namespace HostelBot.Domain.Domain
{
    public class Resident : Entity<string, Resident>, ICanFill
    {
        [QuestionAttribute("Имя", ViewType.TextEnter)]
        [JsonPropertyName("Name")]
        public string Name { get; set; }
        
        [QuestionAttribute("Фамилия", ViewType.TextEnter)]
        [JsonPropertyName("Surname")]
        public string Surname { get; private set; }

        public override string ToString() => $"{Name} {Surname}";
        public IReadOnlyCollection<PropertyInfo> GetFields() => Properties;

    }
}