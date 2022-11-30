using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using HostelBot.Domain.Infrastructure;

namespace HostelBot.Domain.Domain;

public class Room : Entity<Room, int>
{
    public new int Id => Number;
    
    public Room(int number, string hostelName)
    {
        Number = number;
        HostelName = hostelName;
    }
    
    [Key]
    [Question("Комната", ViewType.TextEnter)]
    [JsonPropertyName("Room")]
    public int Number { get; }
    
    public string HostelName { get; }
}