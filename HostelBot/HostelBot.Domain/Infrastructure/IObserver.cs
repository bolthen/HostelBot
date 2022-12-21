namespace HostelBot.Domain.Infrastructure;

public interface IObserver<in T>
{
    void OnNext(T value);
    //void OnError(Exception error);
    void OnCompleted(T value);
}