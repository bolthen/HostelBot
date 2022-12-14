using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Text.Json.Serialization;
using HostelBot.Domain.Infrastructure;

namespace HostelBot.Domain.Domain
{
    public class Resident : Entity<Resident>, IFillable
    {
        public Resident(int telegramId, string name, string surname, Hostel hostel, Room room)
        {
            Id = telegramId;
            Name = name;
            Surname = surname;
            Hostel = hostel;
            Room = room;
            Hostel = hostel;
        }
        
        public Resident(){}
        
        [Question("Имя", ViewType.TextEnter)]
        [JsonPropertyName("Name")]
        [Required, RegularExpression(@"^([А-ЩЭ-Я][а-я]+-?)+$",
             ErrorMessage = "Имя должно начинаться с заглавной буквы, не иметь пробелов")]
        public string Name { get; set; }
        
        [Question("Фамилия", ViewType.TextEnter)]
        [JsonPropertyName("Surname")]
        [Required, RegularExpression(@"^([А-ЩЭ-Я][а-я]+-?)+$",
             ErrorMessage = "Фамилия должна начинаться с заглавной буквы, не иметь пробелов")]
        public string Surname { get; set; }
        
        public Room? Room { get; set; }
        
        public Hostel? Hostel { get; set; }

        public override string ToString() => $"{Name} {Surname}";
        
        [NotMapped]
        public bool Filled { get; set; }
        public IReadOnlyCollection<PropertyInfo> GetFields() => Properties;

    }
}