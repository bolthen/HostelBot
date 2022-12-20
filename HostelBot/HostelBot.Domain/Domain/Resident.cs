using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Text.Json.Serialization;
using HostelBot.Domain.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HostelBot.Domain.Domain
{
    public class Resident : Entity<Resident>
    {
        public Resident(long telegramId, string name, string surname, Hostel hostel, Room room)
        {
            Id = telegramId;
            Name = name;
            Surname = surname;
            Hostel = hostel;
            Room = room;
            Hostel = hostel;
        }

        public Resident(){}

        public string Name { get; set; }
        
        public string Surname { get; set; }

        public Room? Room { get; set; }
        
        public Hostel? Hostel { get; set; }

        public List<Utility> Utilities { get; set; } = new ();

        public List<Appeal> Appeals { get; set; } = new();

        public bool IsAccepted { get; set; }
        
        public override string ToString() => $"{Name} {Surname}";

        public void AddUtility(Utility utility)
        {
            Utilities.Add(utility);
        }
        
        public void AddAppeal(Appeal appeal)
        {
            Appeals.Add(appeal);
        }
        
        public IReadOnlyCollection<PropertyInfo> GetFields() => Properties;
    }
}