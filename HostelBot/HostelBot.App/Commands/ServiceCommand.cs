using HostelBot.Domain;
using HostelBot.Domain.Domain;
using HostelBot.Domain.Infrastructure;

namespace HostelBot.App;

public class ServiceCommand : FillCommand<Service>
{
    public ServiceCommand(string name, IEnumerable<Manager<Service>> managers) : base(name, managers, new Service(name))
    {
    }
}