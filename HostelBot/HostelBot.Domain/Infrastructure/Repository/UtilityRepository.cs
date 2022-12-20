using HostelBot.Domain.Domain;
using HostelBot.Domain.Infrastructure.Repository;

namespace HostelBot.Domain.Infrastructure.Repository;

public class UtilityRepository : EntityRepository<Utility>
{
    public UtilityRepository(MainDbContext context) : base(context) { }
}