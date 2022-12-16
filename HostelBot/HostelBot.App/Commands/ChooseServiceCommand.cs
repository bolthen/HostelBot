using HostelBot.Domain;
using HostelBot.Domain.Domain;
using HostelBot.Domain.Infrastructure;
using HostelBot.Domain.Infrastructure.Services;

namespace HostelBot.App;

public class ChooseServiceCommand : Command
{
    private readonly IEnumerable<Manager<UtilityFiller>> managers;
    private readonly HostelRepository hostelNameRepository;

    public ChooseServiceCommand(IEnumerable<Manager<UtilityFiller>> managers, HostelRepository hostelNameRepository)
        : base("Услуги")
    {
        this.managers = managers;
        this.hostelNameRepository = hostelNameRepository;
    }
    
    public override List<Command> GetSubcommands(long residentId)
    {
        var names = hostelNameRepository.GetUtilityNames("№6").Result; // TODO

        return names.Select(name => new UtilityCommand(name.Name, managers)).Cast<Command>().ToList();
    }
}