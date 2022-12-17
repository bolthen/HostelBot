using HostelBot.Domain.Infrastructure;
using HostelBot.Domain.Infrastructure.Exceptions;
using HostelBot.Domain.Infrastructure.Services;

namespace HostelBot.App;

public class CheckRegistrationCommand : FillCommand<ResidentFiller>
{
    private List<Command> subcommands;
    private readonly ResidentRepository residentRepository;

    public CheckRegistrationCommand(IEnumerable<Manager<ResidentFiller>> managers, List<Command> subcommands,
        ResidentRepository residentRepository) 
        : base("Проверка регистрации", managers, new ResidentFiller())
    {
        this.subcommands = subcommands;
        this.residentRepository = residentRepository;
    }

    public override List<Command> GetSubcommands(long residentId)
    {
        if (!residentRepository.CheckAsync(residentId).Result)
            throw new NotRegisteredResidentException();
        
        return subcommands;
    }
}