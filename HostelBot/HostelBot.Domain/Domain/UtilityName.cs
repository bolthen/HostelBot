using HostelBot.Domain.Infrastructure;

namespace HostelBot.Domain.Domain;

public class UtilityName : Entity<UtilityName>
{
    public UtilityName()
    {
    }
    
    public UtilityName(string name, string hostelName)
    {
        Name = name;
        HostelName = hostelName;
    }
    
    public string Name { get; set; }
    public string HostelName { get; set; }
}