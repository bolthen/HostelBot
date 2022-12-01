using HostelBot.Domain.Domain;
using HostelBot.Domain.Infrastructure;

namespace HostelBot.App;

public class AppealCommand : FillCommand<Appeal>/*<AppealFiller>*/
{
    public AppealCommand(Appeal filler) : base("Обращение", filler)
    {
    }
}