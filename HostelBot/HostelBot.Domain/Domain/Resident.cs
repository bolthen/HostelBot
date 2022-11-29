using System.Reflection;
using HostelBot.Domain.Infrastructure;

namespace HostelBot.Domain.Domain
{
    public class Resident : ValueType<Resident>, ICanFill
    {
        { "Comment"(JsonPropertyName): "Порвало трубу"}
        [Filled("Имя")]
        [JsonPropertyName("Name")]
        public string Name { get; set; }
        
        [Filled("Опишите вашу проблему")]
        [JsonPropertyName("Surname")]
        public string Surname { get; private set; }

        public override string ToString() => $"{Name} {Surname}";
        public IReadOnlyCollection<PropertyInfo> GetFields() => PropertiesToFill;
    }
}