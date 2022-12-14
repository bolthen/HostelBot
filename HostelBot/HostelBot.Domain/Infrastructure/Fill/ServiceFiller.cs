using System.Text.Json;

namespace HostelBot.Domain.Infrastructure;

public class ServiceFiller : Filler
{
    public ServiceFiller(Utility fillable) : base(fillable)
    {
    }

    public override void HandleFilledClass(IFillable data)
    {
        var filledService = (Utility) data;
    }
}