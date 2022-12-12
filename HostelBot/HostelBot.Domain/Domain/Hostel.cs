using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using HostelBot.Domain.Infrastructure;
using HostelBot.Domain.Infrastructure.Repository;

namespace HostelBot.Domain.Domain;

public class Hostel : Entity<Hostel, string>
{
    public new string Id => Name;

    private readonly List<Resident> residents = new ();
    public IReadOnlyCollection<Resident> Residents => residents.AsReadOnly();
    
    private readonly List<Room> rooms = new ();
    public IReadOnlyCollection<Room> Rooms => rooms.AsReadOnly();
    
    private readonly List<Service> services = new ();
    public IReadOnlyCollection<Service> Services => services.AsReadOnly();

    public Hostel(string name)
    {
        Name = name;
    }
    
    public Hostel()
    {
    }
    
    [Key]
    [Question("Общежитие", ViewType.TextEnter)]
    [JsonPropertyName("Name")]
    public string Name { get; }
    
    // public void AddResident(Resident resident)
    // {
    //     residents.Add(resident);
    // }
    //
    // public void AddRoom(Room room)
    // {
    //     rooms.Add(room);
    // }
}