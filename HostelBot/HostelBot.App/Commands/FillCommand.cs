using HostelBot.Domain.Domain;
using HostelBot.Domain.Infrastructure;

namespace HostelBot.App;

public abstract class FillCommand<TFillable> : Command
    where TFillable : IFillable, Domain.Infrastructure.IObservable<TFillable>, new()
{
    private IEnumerable<Manager<TFillable>> managers;
    protected TFillable Fillable;
    
    public FillCommand(string name, IEnumerable<Manager<TFillable>> managers, TFillable fillable) : base(name)
    {
        this.managers = managers;
        Fillable = fillable;
    }
    
    public override IFillable? GetFillable(long residentId)
    {
        foreach (var manager in managers)
            manager.Subscribe(Fillable);
        Fillable.ResidentId = residentId;
        return Fillable;
    }
}