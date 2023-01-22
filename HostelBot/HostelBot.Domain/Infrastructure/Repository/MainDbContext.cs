using System.Net.Mime;
using HostelBot.Domain.Domain;
using iTextSharp.text.pdf;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;


namespace HostelBot.Domain.Infrastructure.Repository;

public sealed class MainDbContext : DbContext
{
    public DbSet<Hostel> Hostels { get; set; }
    public DbSet<Resident> Residents { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<HostelBot.Domain.Utility> Utilities { get; set; }
    public DbSet<UtilityName> UtilityNames { get; set; }
    public DbSet<Appeal> Appeal { get; set; }
    public DbSet<Administrator> Administrators { get; set; }

    public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    /*public MainDbContext()
    {
        //Database.EnsureDeleted(); // TODO DELETE
        Database.EnsureCreated();
    }*/
    
    public MainDbContext()
    { 
        //Database.EnsureDeleted(); // TODO DELETE
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var workingDirectory = Environment.CurrentDirectory;
        var workingDirectory2 = System.IO.Directory.GetCurrentDirectory();
        Console.WriteLine(workingDirectory);
        var projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.Parent.FullName;
        var absolutePath = Path.Combine(projectDirectory, "helloapp.db");
        optionsBuilder.UseSqlite($"Data Source={absolutePath}");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Hostel>(hostel => 
            hostel
                .HasMany(a => a.Residents)
                .WithOne(x => x.Hostel));
        
        modelBuilder.Entity<Room>()
            .HasIndex(x => x.Number)
            .IsUnique();
        
        modelBuilder.Entity<Hostel>(hostel =>
            hostel
                .HasMany(x => x.Rooms)
                .WithOne(y => y.Hostel));
        
        modelBuilder.Entity<Hostel>(hostel =>
            hostel
                .HasMany(x => x.UtilityNames)
                .WithOne(y => y.Hostel));

        modelBuilder.Entity<Resident>(resident =>
            resident
                .HasMany(x => x.Appeals)
                .WithOne(y => y.Resident));
        
        modelBuilder.Entity<Resident>(resident =>
            resident
                .HasMany(x => x.Utilities)
                .WithOne(y => y.Resident));
    }
}