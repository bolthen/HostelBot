using HostelBot.Domain.Infrastructure.Repository;

namespace HostelBot.Domain.Infrastructure;

public abstract class Manager<T> : IObserver<T>
    where T : IObservable<T>
{
    protected readonly ResidentRepository residentRepository;

    public Manager(ResidentRepository residentRepository)
    {
        this.residentRepository = residentRepository;
    }

    private IDisposable? unsubscriber;
    
    public void Subscribe(T observable)
    {
        unsubscriber = observable.Subscribe(this);
    }
    
    public void OnNext(T value)
    {
        // Здесь наверное можно сохранять промежуточное заполнение, если
        // после заполнения каждого свойства вызывать этот метод.
        // Ещё для этого возможно понадобится сохранять id пользователя внутри Appeal
    }

    public void OnCompleted(T value)
    {
        unsubscriber.Dispose();
        Handle(value);
    }

    protected abstract void Handle(T value);
}