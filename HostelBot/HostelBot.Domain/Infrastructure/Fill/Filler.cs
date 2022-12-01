namespace HostelBot.Domain.Infrastructure;

/*public interface IFiller
{
    ICanFill GetFillClass();
    public void HandleFilledClass(string data);
}*/

public abstract class Filler
{
    private readonly IFillable fillable;

    public Filler(IFillable fillable)
    {
        this.fillable = fillable;
    }

    public virtual IFillable GetFillClass()
    {
        return fillable;
    }
    public abstract void HandleFilledClass(IFillable data);
}