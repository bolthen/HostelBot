using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using HostelBot.Domain.Infrastructure;
using HostelBot.Domain.Infrastructure.Repository;

namespace HostelBot.Domain.Domain;

public class Hostel : Entity<Hostel>
{
    public readonly List<Resident> Residents = new ();

    public readonly List<Room> Rooms = new ();

    public readonly List<Utility> Utilities = new ();

    public Hostel(string name)
    {
        Name = name;
    }
    
    public Hostel()
    {
    }

    public string Name { get; set; }
    
    public void AddResident(Resident resident)
    {
        Residents.Add(resident);
    }
    
    public void AddRoom(Room room)
    {
        Rooms.Add(room);
    }
}