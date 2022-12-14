using HostelBot.Domain.Domain;
using HostelBot.Domain.Infrastructure;

namespace HostelBot.App;

public class ResidentRegistrationCommand : FillCommand<Resident>
{
    public ResidentRegistrationCommand(string name, IEnumerable<Manager<Resident>> managers,
        List<Command> subcommands)
        : base(name, managers, new Resident())
    {
        this.subcommands = subcommands;
    }

    private List<Command> subcommands;

    public override List<Command> GetSubcommands(long residentId)
    {
        return subcommands;
    }
}
