using HostelBot.Domain.Infrastructure;

namespace HostelBot.App;

public class CheckRegistrationCommand : FillCommand<ResidentFiller>
{
    private List<Command> subcommands;
    public CheckRegistrationCommand(IEnumerable<Manager<ResidentFiller>> managers, List<Command> subcommands) 
        : base("Проверка регистрации", managers, new ResidentFiller())
    {
        this.subcommands = subcommands;
    }

    public override List<Command> GetSubcommands(long residentId)
    {
        // кинет какой нибудь exception, если еще не верефицирован
        return subcommands;
    }
}