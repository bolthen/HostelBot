using HostelBot.Domain;
using HostelBot.Domain.Domain;
using HostelBot.Domain.Infrastructure;
using HostelBot.Domain.Infrastructure.Services;

namespace HostelBot.App;

public class ChooseServiceCommand : Command
{
    private readonly IEnumerable<Manager<Utility>> managers;
    private readonly HostelRepository hostelNameRepository;

    public ChooseServiceCommand(IEnumerable<Manager<Utility>> managers, HostelRepository hostelNameRepository)
        : base("Услуги")
    {
        this.managers = managers;
        this.hostelNameRepository = hostelNameRepository;
    }
    
    public override List<Command> GetSubcommands(int residentId)
    {
        var names = hostelNameRepository.GetUtilityNames(residentId).Result;

        return names.Select(name => new ServiceCommand(name.Name, managers)).Cast<Command>().ToList();
    }
}