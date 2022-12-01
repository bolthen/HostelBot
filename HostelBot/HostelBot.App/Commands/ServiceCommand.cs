using HostelBot.Domain;
using HostelBot.Domain.Infrastructure;

namespace HostelBot.App;

public class ServiceCommand : FillCommand<Service>
{
    private readonly IService service;
    
    public ServiceCommand(Service service) : base(service.Name, service)
    {
        this.service = service;
    }
}