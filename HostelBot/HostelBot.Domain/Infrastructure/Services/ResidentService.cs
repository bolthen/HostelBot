using HostelBot.Domain.Domain;
using HostelBot.Domain.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace HostelBot.Domain.Infrastructure.Services;

public class ResidentService : EntityService<Resident>
{
    public ResidentService(EntityRepository<Resident> repository) : base(repository) { }

    public async Task<List<Utility>> GetUtilities(int id)
    {
        return repository.Context.Utilities.Where(x => x.ResidentId == id).ToList();
    }
}