namespace HostelBot.Domain.Infrastructure.Misc.HtmlTableBuilder;

public class HtmlWidthItem
{
    private readonly int width;
    private readonly string content;
    private readonly string tag;
        
    public HtmlWidthItem(int width, string content, string tag)
    {
        this.width = width;
        this.content = content;
        this.tag = tag;
    }

    public override string ToString()
    {
        return $"<{tag} height=\"60\" width=\"{width}%\">" +
               $"{content}" +
               $"</{tag}>";
    }
}