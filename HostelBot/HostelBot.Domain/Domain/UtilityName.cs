using HostelBot.Domain.Infrastructure;

namespace HostelBot.Domain.Domain;

public class UtilityName : Entity<UtilityName>
{
    public UtilityName()
    {
    }

    public UtilityName(string name)
    {
        Name = name;
    }

    public string Name { get; set; }
    
    public Hostel Hostel { get; set; }
}