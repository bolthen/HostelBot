using HostelBot.Domain.Infrastructure;
using HostelBot.Domain.Infrastructure.Exceptions;
using HostelBot.Domain.Infrastructure.Repository;

namespace HostelBot.App;

public class CheckRegistrationCommand : FillCommand<ResidentFillable>
{
    private List<Command> subcommands;
    private readonly ResidentRepository residentRepository;

    public CheckRegistrationCommand(IEnumerable<Manager<ResidentFillable>> managers, List<Command> subcommands,
        ResidentRepository residentRepository) 
        : base("Проверка регистрации", managers, new ResidentFillable())
    {
        this.subcommands = subcommands;
        this.residentRepository = residentRepository;
        AppDomain.CurrentDomain.UnhandledException += ProcessException;
    }

    public override List<Command> GetSubcommands(long residentId)
    {
        if (!residentRepository.CheckAsync(residentId).Result)
            throw new NotRegisteredResidentException();
        if (!residentRepository.GetAsync(residentId).Result.IsAccepted)
            throw new NotAcceptedResidentException();
        
        return subcommands;
    }
    
    static void ProcessException(object sender, UnhandledExceptionEventArgs args)
    {
        Console.WriteLine((args.ExceptionObject as Exception).StackTrace);
        Environment.Exit(1);
    }
}