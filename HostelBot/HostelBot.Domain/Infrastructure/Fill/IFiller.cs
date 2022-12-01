namespace HostelBot.Domain.Infrastructure;

/*public interface IFiller
{
    ICanFill GetFillClass();
    public void HandleFilledClass(string data);
}*/

public interface IFiller
{
    IFillable GetFillClass();
    void HandleFilledClass(IFillable data);
}