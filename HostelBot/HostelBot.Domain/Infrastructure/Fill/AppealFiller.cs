using System.Text.Json;
using System.Xml.Serialization;
using HostelBot.Domain.Domain;

namespace HostelBot.Domain.Infrastructure;

public class AppealFiller : Filler
{
    public AppealFiller(Appeal fillable) : base(fillable)
    {
    }

    public override void HandleFilledClass(IFillable data)
    {
        var filledAppeal = (Appeal) data;
    }
}