using HostelBot.Domain;
using HostelBot.Domain.Domain;
using HostelBot.Domain.Infrastructure;

namespace HostelBot.App;

public class ServiceCommand : FillCommand<Utility>
{
    public ServiceCommand(string name, IEnumerable<Manager<Utility>> managers) 
        : base(name, managers, new Utility(name))
    {
    }
}