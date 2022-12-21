using HostelBot.Domain;
using HostelBot.Domain.Domain;
using HostelBot.Domain.Infrastructure;
using HostelBot.Domain.Infrastructure.Exceptions;
using HostelBot.Domain.Infrastructure.Repository;

namespace HostelBot.App;

public class ChooseServiceCommand : Command
{
    private readonly IEnumerable<Manager<UtilityFillable>> managers;
    private readonly ResidentRepository residentRepository;

    public ChooseServiceCommand(IEnumerable<Manager<UtilityFillable>> managers, ResidentRepository residentRepository)
        : base("Услуги")
    {
        this.managers = managers;
        this.residentRepository = residentRepository;
    }
    
    public override List<Command> GetSubcommands(long residentId)
    {
        Resident resident;
        try
        {
            resident = residentRepository.GetAsync(residentId).Result;
        }
        catch (ArgumentException e)
        {
            throw new NotRegisteredResidentException(e);
        }
        
        var names = resident.Hostel.UtilityNames; // TODO

        return names.Select(name => new UtilityCommand(name.Name, managers)).Cast<Command>().ToList();
    }
}