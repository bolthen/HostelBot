using HostelBot.Domain.Domain;
using HostelBot.Domain.Infrastructure;

namespace HostelBot.App;

public class AppealCommand : FillCommand<AppealFillable>
{
    public AppealCommand(IEnumerable<Manager<AppealFillable>> managers) : base("Обращения", managers, new AppealFillable())
    {
    }
}