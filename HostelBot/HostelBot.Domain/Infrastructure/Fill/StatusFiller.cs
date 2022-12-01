using HostelBot.Domain.Domain;

namespace HostelBot.Domain.Infrastructure;

public class StatusFiller : Filler
{
    public StatusFiller(Status fillable) : base(fillable)
    {
    }

    public override void HandleFilledClass(IFillable data)
    {
        var filledStatus = (Status) data;
    }
}