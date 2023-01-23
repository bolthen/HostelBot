using HostelBot.Domain.Domain;

namespace HostelBot.Domain.Infrastructure;

public class Unsubscriber<T> : IDisposable
{
    private readonly List<IObserver<T>> observers;
    private readonly IObserver<T> observer;
    
    public Unsubscriber(List<IObserver<T>> observers,
        IObserver<T> observer)
    {
        this.observers = observers;
        this.observer = observer;
    }
    
    public void Dispose()
    {
        if (observers.Contains(observer))
            observers.Remove(observer);
    }
}