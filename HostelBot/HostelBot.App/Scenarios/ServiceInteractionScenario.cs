using HostelBot.Domain;
using HostelBot.Domain.Infrastructure;

namespace HostelBot.App;

public class ServiceInteractionScenario : IInteractionScenario//: FilledScenario<ServiceFiller>
{
    private IFiller filler;
    
    public ServiceInteractionScenario(IService service)
    {
        filler = new ServiceFiller(service);
    }
    
    public List<ICommand> GetSubcommands()
    {
        throw new NotImplementedException();
    }

    public object[] GetStaticInfo()
    {
        throw new NotImplementedException();
    }

    public IFiller? GetFiller()
    {
        return filler;
    }
}