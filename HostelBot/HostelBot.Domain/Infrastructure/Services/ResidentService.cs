using HostelBot.Domain.Domain;
using HostelBot.Domain.Infrastructure.Repository;

namespace HostelBot.Domain.Infrastructure.Services;

public class ResidentService : EntityService<Resident>
{
    public ResidentService(EntityRepository<Resident> repository) : base(repository) { }
}