using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Text.Json.Serialization;
using HostelBot.Domain.Infrastructure;

namespace HostelBot.Domain;

public class Utility : Entity<Utility>, IService, Infrastructure.IObservable<Utility>
{
    public Utility() { }
    
    public Utility(string name) => Name = name;

    public string Name { get; set; }
    
    [Question("Опишите Вашу проблему", ViewType.TextEnter)]
    [JsonPropertyName("Content")]
    public string Content { get; set; }

    public int ResidentId { get; set; }
    
    public IReadOnlyCollection<PropertyInfo> GetFields() => Properties;
    private readonly List<Infrastructure.IObserver<Utility>> observers = new();
    
    /*private bool filled;
    [NotMapped]
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
    
    public IDisposable Subscribe(Infrastructure.IObserver<Utility> observer)
    {
        if (!observers.Contains(observer))
            observers.Add(observer);
        return new Unsubscriber<Utility>(observers, observer);
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