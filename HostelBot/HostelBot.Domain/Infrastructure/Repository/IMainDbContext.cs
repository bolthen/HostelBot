namespace HostelBot.Domain.Infrastructure.Repository;
using HostelBot.Domain.Domain;
using Microsoft.EntityFrameworkCore;

public abstract class IMainDbContext : DbContext
{
    public DbSet<Hostel> Hostels { get; set; }
    public DbSet<Resident> Residents { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Utility> Utilities { get; set; }
    public DbSet<UtilityName> UtilityNames { get; set; }
    public DbSet<Appeal> Appeal { get; set; }
    public DbSet<Administrator?> Administrators { get; set; }
    
    public IMainDbContext(DbContextOptions<MainDbContext> options) : base(options) { }
    
    public IMainDbContext(){}
}