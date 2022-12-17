using System.Reflection;

namespace HostelBot.Domain.Infrastructure;

public abstract class Filler<T> : DddObject<T>, IObservable<T>, IFillable
    where T : DddObject<T>
{
    protected readonly List<IObserver<T>> observers = new();
    
    public IDisposable Subscribe(IObserver<T> observer)
    {
        if (!observers.Contains(observer))
            observers.Add(observer);
        return new Unsubscriber<T>(observers, observer);
    }
    
    /*public IDisposable Subscribe(IObserver<T> observer)
    {
        if (!observers.Contains(observer))
            observers.Add(observer);
        return new Unsubscriber<T>(observers, observer);
    }*/

    public abstract void OnFilled();
    /*{
        foreach (var observer in observers.ToArray())
            observer.OnCompleted(this);
    }*/

    public IReadOnlyCollection<PropertyInfo> GetFields() => Properties;

    public long ResidentId { get; set; }

    //public abstract TEntity GetFilledEntity(Manager<TEntity> manager);
}