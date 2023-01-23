using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using HostelBot.Domain.Infrastructure;
using HostelBot.Domain.Infrastructure.Repository;

namespace HostelBot.Domain.Domain;

public class Hostel : Entity<Hostel>
{
    public List<Resident> Residents { get; set; } = new ();

    public List<Room> Rooms { get; set; } = new ();

    public List<UtilityName> UtilityNames { get; set; } = new ();

    public Hostel(string name)
    {
        Name = name;
    }
    
    public Hostel()
    {
    }

    public string Name { get; set; }

    public void AddUtilityName(UtilityName utilityName)
    {
        UtilityNames.Add(utilityName);
    }

    public void AddResident(Resident resident)
    {
        Residents.Add(resident);
    }
    
    public void AddRoom(Room room)
    {
        if (!Rooms.Contains(room))
            Rooms.Add(room);
    }
}