using HostelBot.Domain.Domain;
using HostelBot.Domain.Infrastructure.Repository;

namespace HostelBot.Domain.Infrastructure.Services;

public class UtilityService : EntityService<Utility>
{
    public UtilityService(EntityRepository<Utility> repository) : base(repository) { }

    
}