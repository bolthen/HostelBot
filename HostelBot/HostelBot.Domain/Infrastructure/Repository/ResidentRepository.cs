using HostelBot.Domain.Domain;
using Microsoft.EntityFrameworkCore;

namespace HostelBot.Domain.Infrastructure.Repository;

public class ResidentRepository : EntityRepository<Resident>
{
    public ResidentRepository(IMainDbContext context) : base(context) {}

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
        throw new ArgumentException($"The Resident with the given id {id} was not found in the database");
    }

    public async new Task<bool> CheckAsync(long id)
    {
        var foundEntity = context.Residents.FirstOrDefault(r => r.Id == id);

        return foundEntity != null;
    }
}