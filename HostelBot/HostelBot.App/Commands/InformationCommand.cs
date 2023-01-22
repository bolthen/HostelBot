using HostelBot.Domain.Infrastructure;

namespace HostelBot.App;

public class InformationCommand : Command
{
    public InformationCommand() : base("Информация")
    {
    }

    public override string? GetStaticInfo(long residentId)
    {
        return "Информация";
    }
}