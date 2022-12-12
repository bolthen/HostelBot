
using HostelBot.Domain.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;


namespace HostelBot.Domain.Infrastructure.Repository;

public sealed class MainDbContext : DbContext
{
    public DbSet<Hostel> Hostels { get; set; }
    public DbSet<Resident> Residents { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Service> Services { get; set; }
    
    public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    public MainDbContext() => Database.EnsureCreated();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=helloapp.db");
    }
}