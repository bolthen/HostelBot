using HostelBot.Domain.Domain;
using HostelBot.Domain.Infrastructure;

namespace HostelBot.App;

public class ResidentRegistrationCommand : FillCommand<Resident>
{
    private List<Command> subcommands;

    public ResidentRegistrationCommand(IEnumerable<Manager<Resident>> managers, List<Command> subcommands)
        : base("Регистрация", managers, new Resident())
    {
        this.subcommands = subcommands;
    }
    

    public override List<Command> GetSubcommands(long residentId)
    {
        return subcommands;
    }
}
