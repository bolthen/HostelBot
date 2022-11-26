namespace HostelBot.Domain;

public class FilledAttribute : Attribute
{
    public readonly string Header;
        
    public FilledAttribute(string header)
    {
        Header = header;
    }
}