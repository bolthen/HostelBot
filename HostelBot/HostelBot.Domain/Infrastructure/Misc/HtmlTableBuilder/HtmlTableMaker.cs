using System.Text;

namespace HostelBot.Domain.Infrastructure.Misc.HtmlTableBuilder;

public class HtmlStatTableMaker : IDisposable
{
    private readonly StringBuilder data;

    public HtmlStatTableMaker(StringBuilder data, string caption, DateTime startDate, DateTime endDate)
    {
        this.data = data;
        this.data.Append($"<big>{caption}:</big><br/>");
        if (startDate == endDate)
            this.data.Append($"<big>{startDate.ToString("dd.MM.yyyy")}</big>");
        else
            this.data.Append($"<big>{startDate.ToString("dd.MM.yyyy")} — {endDate.ToString("dd.MM.yyyy")}</big>");
        this.data.Append("<table>");
    }

    public HtmlTableHeader AddHeader() => new (data);
    public HtmlRow AddRow() => new (data);
    public void StartBody() => data.Append("<tbody>");
    public void EndBody() => data.Append("</tbody>");

    public void Dispose() => data.Append("</table>");
}