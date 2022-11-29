using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text.Json.Serialization;
using HostelBot.Domain.Infrastructure;

namespace HostelBot.Domain.Domain
{
    public class Resident : Entity<Resident, int>, ICanFill
    {
        public Resident(string name, string surname)
        {
            Name = name;
            Surname = surname;
        }
        
        [Question("Имя", ViewType.TextEnter)]
        [JsonPropertyName("Name")]
        [Required, RegularExpression(@"^([А-ЩЭ-Я][а-я]+-?)+$",
             ErrorMessage = "Имя должно начинаться с заглавной буквы, не иметь пробелов")]
        public string Name { get; }
        
        [Question("Фамилия", ViewType.TextEnter)]
        [JsonPropertyName("Surname")]
        [Required, RegularExpression(@"^([А-ЩЭ-Я][а-я]+-?)+$",
             ErrorMessage = "Фамилия должна начинаться с заглавной буквы, не иметь пробелов")]
        public string Surname { get; }

        public override string ToString() => $"{Name} {Surname}";
        public IReadOnlyCollection<PropertyInfo> GetFields() => Properties;

    }
}