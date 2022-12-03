using System.Reflection;
using System.Text.Json.Serialization;
using HostelBot.Domain.Infrastructure;

namespace HostelBot.Domain;

public class Service : Entity<Service, int>, IService, Infrastructure.IObservable<Service>
{
    public Service()
    {
    }
    
    public Service(string name) => Name = name;

    public string Name { get; private set; }
    
    [Question("Опишите Вашу проблему", ViewType.TextEnter)]
    [JsonPropertyName("Content")]
    public string Content { get; set; }
    
    public IReadOnlyCollection<PropertyInfo> GetFields() => Properties;
    private readonly List<Infrastructure.IObserver<Service>> observers = new();
    
    private bool filled;
    public bool Filled
    {
        get => filled;
        set
        {
            filled = value;
            if (value)
                OnCompleted();
        }
    }
    
    public IDisposable Subscribe(Infrastructure.IObserver<Service> observer)
    {
        if (!observers.Contains(observer))
            observers.Add(observer);
        return new Unsubscriber<Service>(observers, observer);
    }
    
    private void OnCompleted()
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