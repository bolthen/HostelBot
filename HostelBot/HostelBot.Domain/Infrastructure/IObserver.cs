namespace HostelBot.Domain.Infrastructure;

public interface IObserver<in T>
{
    Task OnCompleted(T value);
}