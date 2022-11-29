namespace HostelBot.App;

public class Service : IService
{
    public Service(string name)
    {
        Name = name;
    }
    
    public string Name { get; private set; }
}