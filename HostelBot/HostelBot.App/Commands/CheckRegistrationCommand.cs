namespace HostelBot.App;

public class CheckRegistrationCommand : Command
{
    private List<Command> subcommands;
    public CheckRegistrationCommand(List<Command> subcommands) : base("Проверка регистрации")
    {
        this.subcommands = subcommands;
    }

    public override List<Command> GetSubcommands(long residentId)
    {
        // кинет какой нибудь exception, если еще не верефицирован
        return subcommands;
    }
}