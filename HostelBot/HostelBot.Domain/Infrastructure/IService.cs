using HostelBot.Domain.Infrastructure;

namespace HostelBot.Domain;

public interface IService : IFillable
{
    string Name { get; }
}