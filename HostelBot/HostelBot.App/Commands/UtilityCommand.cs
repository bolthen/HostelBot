using HostelBot.Domain;
using HostelBot.Domain.Domain;
using HostelBot.Domain.Infrastructure;

namespace HostelBot.App;

public class UtilityCommand : FillCommand<UtilityFiller>
{
    public UtilityCommand(string name, IEnumerable<Manager<UtilityFiller>> managers) 
        : base(name, managers, new UtilityFiller(name))
    {
    }
}