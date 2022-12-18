/*using HostelBot.Domain.Domain;
using HostelBot.Domain.Infrastructure;

namespace HostelBot.App;

public class ResidentRegistrationCommand : FillCommand<ResidentFiller>
{
    private List<Command> subcommands;

    public ResidentRegistrationCommand(IEnumerable<Manager<ResidentFiller>> managers, List<Command> subcommands)
        : base("Регистрация", managers, new ResidentFiller())
    {
        this.subcommands = subcommands;
    }
    

    public override List<Command> GetSubcommands(long residentId)
    {
        return subcommands;
    }
}*/
