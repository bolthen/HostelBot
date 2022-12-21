using System.Text;

namespace HostelBot.Domain.Infrastructure.Misc.HtmlTableBuilder;

public class HtmlRow : IDisposable
{
    private readonly StringBuilder data;
        
    public HtmlRow(StringBuilder data)
    {
        this.data = data;
        this.data.Append("<tr>");
    }

    public void AddCell(string content, int width) 
        => data.Append(new HtmlWidthItem(width, content, "td"));
        //=> data.Append($"<td class=\"col-{widthItem.Width}\">{widthItem.Content}</td>\n");
        //=> data.Append($"<td>{widthItem.Content}</td>");

    public void Dispose() => data.Append("</tr>");
}