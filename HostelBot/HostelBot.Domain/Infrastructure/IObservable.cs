namespace HostelBot.Domain.Infrastructure;

public interface IObservable<out T>
{
    IDisposable Subscribe(IObserver<T> observer);
}