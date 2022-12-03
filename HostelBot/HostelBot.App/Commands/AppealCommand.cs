using HostelBot.Domain.Domain;
using HostelBot.Domain.Infrastructure;

namespace HostelBot.App;

public class AppealCommand : FillCommand<Appeal>
{
    public AppealCommand(/*Appeal filler*/) : base("Обращение"/*, filler*/)
    {
    }

    public override IFillable? GetFillable()
    {
        var observer = new AppealManager();
        var appeal = new Appeal();
        observer.Subscribe(appeal);
        return appeal;
    }
}