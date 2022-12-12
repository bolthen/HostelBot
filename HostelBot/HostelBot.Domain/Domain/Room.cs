using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using HostelBot.Domain.Infrastructure;

namespace HostelBot.Domain.Domain;

public class Room : Entity<Room, int>
{
    public Room(int number, Hostel? hostel)
    {
        Number = number;
        Hostel = hostel;
    }
    
    public Room(){}

    [Question("Комната", ViewType.TextEnter)]
    [JsonPropertyName("Room")]
    public int Number { get; }
    
    [ForeignKey("Hostel")]
    public Hostel? Hostel { get; }
}