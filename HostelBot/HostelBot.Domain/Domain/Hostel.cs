using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using HostelBot.Domain.Infrastructure;

namespace HostelBot.Domain.Domain;

public class Hostel : Entity<Hostel, string>
{
    public new string Id => Name;

    private List<Resident> residents = new List<Resident>();

    public Hostel(string name)
    {
        Name = name;
    }
    
    [Key]
    [Question("Общежитие", ViewType.TextEnter)]
    [JsonPropertyName("Name")]
    public string Name { get; }

    public IReadOnlyCollection<Resident> Residents => residents.AsReadOnly();

    public void AddResident(Resident resident)
    {
        residents.Add(resident);
    }
}