using HostelBot.Domain.Domain;
using HostelBot.Domain.Infrastructure.Repository;

namespace HostelBot.Domain.Infrastructure.Services;

public class ResidentRepository : EntityRepository<Resident>
{
    public ResidentRepository(MainDbContext context) : base(context) { }
}