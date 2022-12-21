using System.Text;

namespace HostelBot.Domain.Infrastructure.Misc.HtmlTableBuilder;

public class HtmlTableHeader : IDisposable
{
    private readonly StringBuilder data;
        
    public HtmlTableHeader(StringBuilder data)
    {
        this.data = data;
        this.data.Append("<thead><tr>");
    }

    public void AddCell(string content, int width) 
        => data.Append(new HtmlWidthItem(width, content, "th"));
        //=> data.Append($"<th>{widthItem.Content}</th>");

    public void Dispose() => data.Append("</tr></thead>");
}