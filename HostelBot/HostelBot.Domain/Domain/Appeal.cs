using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Text.Json.Serialization;
using HostelBot.Domain.Infrastructure;

namespace HostelBot.Domain.Domain
{
    public class Appeal : Entity<Appeal>, IFillable, Infrastructure.IObservable<Appeal>
    {
        public Resident Resident { get; set;}
        
        public IReadOnlyCollection<PropertyInfo> GetFields() => Properties;

        public Appeal() { }
    
        public Appeal(string name, Resident resident)
        {
            Resident = resident;
            Name = name;
        }
        
        [Question("Как вас зовут", ViewType.TextEnter)]
        [JsonPropertyName("Name")]
        [RegularExpression(@"^([А-ЩЭ-Я][а-я]+-?)+$",
            ErrorMessage = "Имя должно начинаться с заглавной буквы, не иметь пробелов")]
        public string Name { get; set; }

        [Question("Опишите Вашу проблему", ViewType.TextEnter)]
        [JsonPropertyName("Content")]
        public string Content { get; set; }

        public long ResidentId { get; set; }

        /*private bool filled;
        public bool Filled
        {
            get => filled;
            set
            {
                filled = value;
                if (value)
                    OnFilled();
            }
        }*/
        
        private readonly List<Infrastructure.IObserver<Appeal>> observers = new();

        public IDisposable Subscribe(Infrastructure.IObserver<Appeal> observer)
        {
            if (!observers.Contains(observer))
                observers.Add(observer);
            return new Unsubscriber<Appeal>(observers, observer);
        }
        
        public void OnFilled()
        {
            foreach (var observer in observers.ToArray())
                observer.OnCompleted(this);
        }

        private void OnNext()
        {
            foreach (var observer in observers)
                observer.OnNext(this);
        }
    }
}