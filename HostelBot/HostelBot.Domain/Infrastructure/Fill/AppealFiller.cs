using System.Text.Json;
using System.Xml.Serialization;
using HostelBot.Domain.Domain;

namespace HostelBot.Domain.Infrastructure;

public class AppealFiller : IFiller
{
    public ICanFill GetFillClass()
    {
        return new Appeal();
    }

    public void HandleFilledClass(string data)
    {
        JsonSerializer.Deserialize<Appeal>(data);
    }
}