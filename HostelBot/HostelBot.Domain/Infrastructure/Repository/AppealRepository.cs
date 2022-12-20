using HostelBot.Domain.Domain;

namespace HostelBot.Domain.Infrastructure.Repository;

public class AppealRepository : EntityRepository<Appeal>
{
    public AppealRepository(MainDbContext context) : base(context) { }
}