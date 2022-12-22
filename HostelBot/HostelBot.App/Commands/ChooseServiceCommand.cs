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
    private readonly HostelRepository hostelRepository;

    public ChooseServiceCommand(IEnumerable<Manager<UtilityFillable>> managers, ResidentRepository residentRepository,
        HostelRepository hostelRepository)
        : base("Услуги")
    {
        this.managers = managers;
        this.residentRepository = residentRepository;
        this.hostelRepository = hostelRepository;
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
        
        var names = hostelRepository.GetAsync(resident.Hostel.Id).Result.UtilityNames;

        return names.Select(name => new UtilityCommand(name.Name, managers)).Cast<Command>().ToList();
    }
}