using HostelBot.Domain.Domain;
using HostelBot.Domain.Infrastructure;

namespace HostelBot.App;

public class AppealCommand : FillCommand<AppealFiller>
{
    public AppealCommand(IEnumerable<Manager<AppealFiller>> managers) : base("Обращения", managers, new AppealFiller())
    {
    }
}