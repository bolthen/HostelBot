using HostelBot.Domain.Infrastructure;

namespace HostelBot.App;

public abstract class FillCommand<TFillable> : Command
    where TFillable : IFillable/*<TFiller> : Command
    where TFiller : Filler<IFillable>*/
{
    private TFillable filler;
    
    public FillCommand(string name, TFillable filler) : base(name)
    {
        this.filler = filler;
    }
    
    public override IFillable? GetFillable()
    {
        return filler;
    }
}