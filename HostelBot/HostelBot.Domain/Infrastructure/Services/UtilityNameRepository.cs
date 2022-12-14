using HostelBot.Domain.Domain;
using HostelBot.Domain.Infrastructure.Repository;

namespace HostelBot.Domain.Infrastructure.Services;

public class UtilityNameRepository : EntityRepository<UtilityName>
{
    public UtilityNameRepository(MainDbContext context) : base(context) { }
}