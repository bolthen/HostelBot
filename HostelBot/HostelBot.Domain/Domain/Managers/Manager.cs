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

    public async Task OnCompleted(T value)
    {
        unsubscriber.Dispose();
        await Handle(value);
    }

    protected abstract Task Handle(T value);
}