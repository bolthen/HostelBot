using HostelBot.Domain.Domain;

namespace HostelBot.Domain.Infrastructure.Repository;

public class CoreRepository
{
    private MainDbContext context;
    
    public EntityRepository<Resident, int> Residents { get; set; }
    
    public EntityRepository<Hostel, string> Hostels { get; set; }
    
    public EntityRepository<Room, int> Rooms { get; set; }
    
    public EntityRepository<Service, string> Services { get; set; }

    public CoreRepository(MainDbContext mainDbContext, EntityRepository<Resident, int> residents,
        EntityRepository<Hostel, string> hostels, EntityRepository<Room, int> rooms,
        EntityRepository<Service, string> services)
    {
        context = mainDbContext;
        Residents = residents;
        Hostels = hostels;
        Rooms = rooms;
        Services = services;
    }

    // public async Task<Hostel?> GetHostelByName(string hostelName)
    // {
    //     return await context.Set<Hostel>();
    // }
}