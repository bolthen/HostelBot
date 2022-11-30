using HostelBot.Domain.Infrastructure;

namespace HostelBot.Domain;

public interface IService : ICanFill
{
    string Name { get; }
}