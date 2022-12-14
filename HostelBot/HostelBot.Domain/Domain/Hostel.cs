using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using HostelBot.Domain.Infrastructure;
using HostelBot.Domain.Infrastructure.Repository;

namespace HostelBot.Domain.Domain;

public class Hostel : Entity<Hostel>
{
    private readonly List<Resident> residents = new ();
    public IReadOnlyCollection<Resident> Residents => residents.AsReadOnly();
    
    private readonly List<Room> rooms = new ();
    public IReadOnlyCollection<Room> Rooms => rooms.AsReadOnly();
    
    private readonly List<Utility> utilities = new ();
    public IReadOnlyCollection<Utility> Utilities => utilities.AsReadOnly();

    public Hostel(string name)
    {
        Name = name;
    }
    
    public Hostel()
    {
    }
    
    [Question("Общежитие", ViewType.TextEnter)]
    [JsonPropertyName("Name")]
    public string Name { get; set; }
    
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