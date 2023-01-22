using HostelBot.Domain.Domain;
using HostelBot.Domain.Infrastructure.Repository;

namespace HostelBot.Domain.Infrastructure.Repository;

public class UtilityNameRepository : EntityRepository<UtilityName>
{
    public UtilityNameRepository(IMainDbContext context) : base(context) { }
}