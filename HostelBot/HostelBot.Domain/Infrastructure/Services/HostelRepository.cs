using HostelBot.Domain.Domain;
using HostelBot.Domain.Infrastructure.Repository;
using iTextSharp.text.pdf;
using Microsoft.EntityFrameworkCore;

namespace HostelBot.Domain.Infrastructure.Services;

public class HostelRepository : EntityRepository<Hostel>
{
    public HostelRepository(MainDbContext context) : base(context) { }

    public async Task<Hostel> GetByName(string hostelName)
    {
        return context.Hostels.FirstOrDefault(x => x.Name == hostelName);
    }

    public async new Task<Hostel> GetAsync(long id)
    {
        var foundEntity = context.Hostels
            .Include(x => x.Residents)
            .Include(x => x.UtilityNames)
            .Include(x => x.Rooms)
            .FirstOrDefault(r => r.Id == id);

        if (foundEntity != null) return foundEntity;

        context.Entry(foundEntity).State = EntityState.Detached;
        throw new Exception($"The Hostel with the given id was not found in the database");
    }                                                                                                 
}