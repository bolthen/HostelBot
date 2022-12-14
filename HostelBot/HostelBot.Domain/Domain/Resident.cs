using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Text.Json.Serialization;
using HostelBot.Domain.Infrastructure;
using HostelBot.Domain.Infrastructure.Services;

namespace HostelBot.Domain.Domain
{
    public class Resident : Entity<Resident>, IFillable, Infrastructure.IObservable<Resident>
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
        
        public List<Utility> Utilities { get; set; }
        
        public List<Appeal> Appeals { get; set; }
        
        public long ResidentId { get; set; }
        
        private readonly List<Infrastructure.IObserver<Resident>> observers = new();
        public IDisposable Subscribe(Infrastructure.IObserver<Resident> observer)
        {
            if (!observers.Contains(observer))
                observers.Add(observer);
            return new Unsubscriber<Resident>(observers, observer);
        }

        public void OnFilled()
        {
            foreach (var observer in observers.ToArray())
                observer.OnCompleted(this);
        }
        
        public override string ToString() => $"{Name} {Surname}";

        public void AddUtility(Utility utility)
        {
            Utilities.Add(utility);
        }
        
        public void AddAppeal(Appeal appeal)
        {
            Appeals.Add(appeal);
        }
        
        public IReadOnlyCollection<PropertyInfo> GetFields() => Properties;

    }
}