using System.Text;

namespace HostelBot.Domain.Infrastructure.Misc.HtmlTableBuilder;

public class HtmlStatTableMaker : IDisposable
{
    private readonly StringBuilder data;

    public HtmlStatTableMaker(StringBuilder data, string caption)
    {
        this.data = data;
        this.data.Append($"<h1>{caption}</h1>");
        this.data.Append("<table>");
    }

    public HtmlTableHeader AddHeader() => new (data);
    public HtmlRow AddRow() => new (data);
    public void StartBody() => data.Append("<tbody>");
    public void EndBody() => data.Append("</tbody>");

    public void Dispose() => data.Append("</table>");
}