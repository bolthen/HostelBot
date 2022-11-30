namespace HostelBot.Domain.Infrastructure;

/*public interface IFiller
{
    ICanFill GetFillClass();
    public void HandleFilledClass(string data);
}*/

public interface IFiller
{
    ICanFill GetFillClass();
    void HandleFilledClass(string data);
}