using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text.Json.Serialization;
using HostelBot.Domain.Infrastructure;

namespace HostelBot.Domain.Domain
{
    public class Appeal : Entity<Appeal, string>, IFillable
    {
        public IReadOnlyCollection<PropertyInfo> GetFields() => Properties;
        
        [Question("Как вас зовут", ViewType.TextEnter)]
        [JsonPropertyName("Name")]
        [RegularExpression(@"^([А-ЩЭ-Я][а-я]+-?)+$",
            ErrorMessage = "Имя должно начинаться с заглавной буквы, не иметь пробелов")]
        public string Name { get; set; }
        
        [Question("Опишите Вашу проблему", ViewType.TextEnter)]
        [JsonPropertyName("Content")]
        public string Content { get; set; }
    }
}