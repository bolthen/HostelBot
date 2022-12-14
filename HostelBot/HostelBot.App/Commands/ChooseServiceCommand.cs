using HostelBot.Domain;
using HostelBot.Domain.Domain;
using HostelBot.Domain.Infrastructure;

namespace HostelBot.App;

public class ChooseServiceCommand : Command
{
    private readonly IEnumerable<Manager<Utility>> managers;

    public ChooseServiceCommand(IEnumerable<Manager<Utility>> managers): base("Услуги")
    {
        this.managers = managers;
    }
    
    public override List<Command> GetSubcommands()
    {
        var names = new List<string> {"Клининг", "Сантехник", "Электрик"};

        return names.Select(name => new ServiceCommand(name, managers)).Cast<Command>().ToList();
    }
}