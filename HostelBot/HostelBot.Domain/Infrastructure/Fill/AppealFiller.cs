using System.Text.Json;
using System.Xml.Serialization;
using HostelBot.Domain.Domain;

namespace HostelBot.Domain.Infrastructure;

public class AppealFiller : IFiller
{
    public IFillable GetFillClass()
    {
        return new Appeal();
    }

    public void HandleFilledClass(string data)
    {
        var a = JsonSerializer.Deserialize<Appeal>(data);
        var temp = 1;
    }
}