using HostelBot.Domain.Domain;
using HostelBot.Domain.Infrastructure.Repository;

namespace HostelBot.Domain.Infrastructure.Services;

public class HostelService : EntityService<Hostel>
{
    public HostelService(EntityRepository<Hostel> repository) : base(repository) { }
}