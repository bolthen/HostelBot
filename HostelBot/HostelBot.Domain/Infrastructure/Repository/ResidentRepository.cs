using HostelBot.Domain.Domain;
using HostelBot.Domain.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace HostelBot.Domain.Infrastructure.Repository;

public class ResidentRepository : EntityRepository<Resident>
{
    public ResidentRepository(MainDbContext context) : base(context) {}

    public async Task<IQueryable<Resident>> GetAll()
    {
        return context.Residents;
    }
    
    public async new Task<Resident> GetAsync(long id)
    {
        var foundEntity = context.Residents
            .Include(x => x.Appeals)
            .Include(x => x.Utilities)
            .Include(x => x.Hostel)
            .Include(x => x.Room)
            .FirstOrDefault(r => r.Id == id);

        if (foundEntity != null) return foundEntity;
        
        context.Entry(foundEntity).State = EntityState.Detached;
        throw new Exception($"The Resident with the given id was not found in the database");
    }
}