using HostelBot.Domain.Domain;
using HostelBot.Domain.Infrastructure.Repository;
using iTextSharp.text.pdf;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Utilities;

namespace HostelBot.Domain.Infrastructure.Repository;

public class HostelRepository : EntityRepository<Hostel>
{
    public HostelRepository(){}
    public async Task<Hostel> GetByName(string hostelName)
    {
        await using IMainDbContext context = new MainDbContext();
        return context.Set<Hostel>()
            .Include(x => x.Residents)
            .Include(x => x.UtilityNames)
            .Include(x => x.Rooms)
            .FirstOrDefault(r => r.Name == hostelName)!;
    }
    
    public new async Task<Hostel> GetAsync(long id)
    {
        await using IMainDbContext context = new MainDbContext();
        var foundEntity = context.Hostels
            .Include(x => x.Residents)
            .Include(x => x.UtilityNames)
            .Include(x => x.Rooms)
            .FirstOrDefault(r => r.Id == id);

        if (foundEntity != null) 
            return foundEntity;
        
        context.Entry(foundEntity).State = EntityState.Detached;
        throw new Exception($"The Hostel with the given id was not found in the database");
    }

    public async Task<Hostel> DeleteResident(long residentId, long hostelId)
    {
        await using IMainDbContext context = new MainDbContext();
        var foundEntity = await context.Set<Resident>().FindAsync(residentId);

        if (foundEntity == null)
            return await GetAsync(hostelId);

        context.Set<Resident>().Remove(foundEntity);
        await context.SaveChangesAsync();
        return await GetAsync(hostelId);
    }

    public async Task<Hostel> AcceptResident(long residentId, long hostelId)
    {
        await using IMainDbContext context = new MainDbContext();
        var foundEntity = await context.Set<Resident>().FindAsync(residentId);
        
        if (foundEntity == null)
            return await GetAsync(hostelId);
        
        foundEntity.IsAccepted = true;
        context.Set<Resident>().Update(foundEntity);
        await context.SaveChangesAsync();
        
        return await GetAsync(hostelId);
        
    }
    
    public async Task<Room> FindOrCreateRoom(string hostelName, int roomNumber)
    {
        var hostel = await GetByName(hostelName);
        if (hostel is null)
            throw new ArgumentException($"No hostel with name {hostelName} in database");
        var room = hostel.Rooms.FirstOrDefault(r => r.Number == roomNumber) ?? new Room(roomNumber, hostel);
        
        hostel.AddRoom(room);
        await UpdateAsync(hostel);

        return room;
    }
    
    public async Task<IEnumerable<Appeal>> GetAppealsByHostelId(long hostelId)
    {
        await using IMainDbContext context = new MainDbContext();
        return context.Set<Appeal>()
            .Include(x => x.Resident)
            .ThenInclude(x => x.Room)
            .Where(x => x.HostelId == hostelId);
    }
    
    public async Task<IEnumerable<Utility>> GetUtilitiesByDate(long hostelId, DateTime start, DateTime end, string utilityName)
    {
        await using IMainDbContext context = new MainDbContext();
        return context.Set<Utility>()
            .Include(x => x.Resident)
            .Where(x => x.HostelId == hostelId)
            .Where(x => x.CreationDateTime >= start && x.CreationDateTime <= end)
            .Where(x => x.Name == utilityName);
    }
}