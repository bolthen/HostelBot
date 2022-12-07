using HostelBot.Domain.Domain;
using HostelBot.Domain.Infrastructure;

namespace HostelBot.App;

public class AppealCommand : FillCommand<Appeal>
{
    public AppealCommand(IEnumerable<Manager<Appeal>> managers) : base("Обращения", managers, new Appeal())
    {
    }
}