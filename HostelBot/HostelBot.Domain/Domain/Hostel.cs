using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using HostelBot.Domain.Infrastructure;

namespace HostelBot.Domain.Domain;

public class Hostel : Entity<Hostel, string>
{
    public new string Id => Name;

    private readonly List<Resident> residents = new ();
    public IReadOnlyCollection<Resident> Residents => residents.AsReadOnly();
    
    private readonly List<Room> rooms = new ();
    public IReadOnlyCollection<Room> Rooms => rooms.AsReadOnly();

    public Hostel(string name)
    {
        Name = name;
    }
    
    [Key]
    [Question("Общежитие", ViewType.TextEnter)]
    [JsonPropertyName("Name")]
    public string Name { get; }
    
    // public void AddResident(int telegramId)
    // {
    //     residents.Add(ResidentsRepository.GetAsync(telegramId));
    // }
    
    // public void AddRoom(int roomId)
    // {
    //     rooms.Add(RoomsRepository.GetAsync(roomId));
    // }
}