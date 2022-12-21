using HostelBot.Domain.Domain;
using HostelBot.Domain.Infrastructure.Repository;
using iTextSharp.text.pdf;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Utilities;

namespace HostelBot.Domain.Infrastructure.Repository;

public class HostelRepository : EntityRepository<Hostel>
{
    public HostelRepository(MainDbContext context) : base(context) { }

    public async Task<Hostel> GetByName(string hostelName)
    {
        return context.Hostels
            .Include(x => x.Residents)
            .Include(x => x.UtilityNames)
            .Include(x => x.Rooms)
            .FirstOrDefault(r => r.Name == hostelName);
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

    public async Task<Hostel> DeleteResident(long residentId, long hostelId)
    {
        var foundEntity = await context.Set<Resident>().FindAsync(residentId);

        if (foundEntity == null)
            return GetAsync(hostelId).Result;

        context.Set<Resident>().Remove(foundEntity);
        await context.SaveChangesAsync();
        return GetAsync(hostelId).Result;
    }

    public async Task<Hostel> AcceptResident(long residentId, long hostelId)
    {
        var foundEntity = await context.Set<Resident>().FindAsync(residentId);
        
        if (foundEntity == null)
            return GetAsync(hostelId).Result;
        
        foundEntity.IsAccepted = true;
        context.Set<Resident>().Update(foundEntity);
        await context.SaveChangesAsync();
        
        return GetAsync(hostelId).Result;
        
    }
    
    public async Task<Room> FindOrCreateRoom(string hostelName, int roomNumber)
    {
        var hostel = GetByName(hostelName).Result;
        if (hostel is null)
            throw new ArgumentException($"No hostel with name {hostelName} in database");
        var room = hostel.Rooms.FirstOrDefault(r => r.Number == roomNumber) ?? new Room(roomNumber, hostel);
        
        hostel.AddRoom(room);
        await UpdateAsync(hostel);

        return room;
    }
    
    public async Task<IReadOnlyCollection<Utility>> GetUtilityByDate(long hostelId, DateTime start, DateTime end, string utilityName)
    {
        var hostel = await GetAsync(hostelId);
        
        return hostel.Residents
            .SelectMany(x => x.Utilities)
            .Where(x =>
            {
                return x.CreationDateTime >= start && x.CreationDateTime <= end && x.Name == utilityName;
            })
            .ToArray();
    }
}