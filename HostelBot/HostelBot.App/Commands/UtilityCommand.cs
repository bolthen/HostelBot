using HostelBot.Domain;
using HostelBot.Domain.Domain;
using HostelBot.Domain.Infrastructure;

namespace HostelBot.App;

public class UtilityCommand : FillCommand<UtilityFillable>
{
    public UtilityCommand(string name, IEnumerable<Manager<UtilityFillable>> managers) 
        : base(name, managers, new UtilityFillable(name))
    {
    }
}