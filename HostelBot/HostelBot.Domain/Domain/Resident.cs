using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Text.Json.Serialization;
using HostelBot.Domain.Infrastructure;

namespace HostelBot.Domain.Domain
{
    public class Resident : Entity<Resident, int>, IFillable
    {
        [Key]
        private int telegramId;
        public new int Id => telegramId; 
        public Resident(int telegramId, string name, string surname, Hostel hostel, Room room)
        {
            this.telegramId = telegramId;
            Name = name;
            Surname = surname;
            Hostel = hostel;
            Room = room;
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
        
        [ForeignKey("Hostel")]
        public Hostel Hostel { get; }
        
        [ForeignKey("Room")]
        public Room Room { get; }

        public override string ToString() => $"{Name} {Surname}";
        public bool Filled { get; set; }
        public IReadOnlyCollection<PropertyInfo> GetFields() => Properties;

    }
}