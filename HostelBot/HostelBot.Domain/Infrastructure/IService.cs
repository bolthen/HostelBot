using HostelBot.Domain.Infrastructure;

namespace HostelBot.App;

public interface IService : ICanFill
{
    string Name { get; }
}