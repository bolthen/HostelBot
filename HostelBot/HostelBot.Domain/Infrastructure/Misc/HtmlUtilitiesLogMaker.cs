using System.Text;
using HostelBot.Domain.Infrastructure.Misc.HtmlTableBuilder;

namespace HostelBot.Domain.Infrastructure.Misc;

public static class HtmlUtilitiesLogMaker
{
    public static string Make(IReadOnlyCollection<Utility> data, DateTime startDate, DateTime endDate)
    {
        var log = new StringBuilder();
        
        using (var table = new HtmlStatTableMaker(log, data.FirstOrDefault()?.Name ?? "unknown", startDate, endDate))
        {
            using (var headers = table.AddHeader())
            {
                headers.AddCell("Комната", 10);
                headers.AddCell("Имя", 20);
                headers.AddCell("Фамилия",20);
                headers.AddCell("Проблема", 20);
                headers.AddCell("Подпись",20);
            }
            
            table.StartBody();
            foreach (var utility in data)
            {
                using var row = table.AddRow();
                row.AddCell(utility.Resident.Room.Number.ToString(), 10);
                row.AddCell(utility.Resident.Name, 20);
                row.AddCell(utility.Resident.Surname, 20);
                row.AddCell(utility.Content, 20);
                row.AddCell("", 20);
            }
            table.EndBody();
        }

        return log.ToString();
    }
}