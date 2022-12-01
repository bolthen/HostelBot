using System.Text.Json;

namespace HostelBot.Domain.Infrastructure;

public class ServiceFiller : IFiller
{
    private IFillable fillable;

    public ServiceFiller(IFillable fillable)
    {
        this.fillable = fillable;
    }
    
    public IFillable GetFillClass()
    {
        return fillable;
    }

    public void HandleFilledClass(IFillable filledClass)
    {
        //do something with filledClass...
        //JsonSerializer.Deserialize<Service>(data);
    }
}