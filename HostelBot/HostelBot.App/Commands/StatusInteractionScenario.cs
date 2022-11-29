using HostelBot.Domain.Infrastructure;

namespace HostelBot.App;

public class StatusInteractionScenario : IInteractionScenario
{
    public List<ICommand> GetSubcommands()
    {
        throw new NotImplementedException();
        /*return new StatusCommand(...);*/
    }
    
    public object[] GetStaticInfo()
    {
        throw new NotImplementedException();
        /*var servicesStatus = BD.getServicesStatus(tgUserId);
        return servicesStatus;*/
    }

    public ICanFill? GetFillClass()
    {
        throw new NotImplementedException();
        //return null;
        //new TFillClass(); // у этого класса атрибуты, его будем Serialize()
    }

    public void HandleFilledClass()
    {
        throw new NotImplementedException();
    }
}