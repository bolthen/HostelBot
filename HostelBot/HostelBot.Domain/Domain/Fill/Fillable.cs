using System.Reflection;

namespace HostelBot.Domain.Infrastructure;

public abstract class Fillable<T> : ValueType<T>, IObservable<T>, IFillable
    where T : ValueType<T>
{
    protected readonly List<IObserver<T>> observers = new();
    
    public IDisposable Subscribe(IObserver<T> observer)
    {
        if (!observers.Contains(observer))
            observers.Add(observer);
        return new Unsubscriber<T>(observers, observer);
    }

    public abstract void OnFilled();

    public long ResidentId { get; set; }
}