namespace HostelBot.Domain.Infrastructure;

public interface IObserver<in T>
{
    void OnCompleted(T value);
}