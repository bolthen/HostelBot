using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Text.Json.Serialization;
using HostelBot.Domain.Infrastructure;

namespace HostelBot.Domain.Domain
{
    public class Appeal : Entity<Appeal>
    {
        public Resident Resident { get; set; }
        
        public string Content { get; set; }
        
        public string? Answer { get; set; }
        
        public Appeal() { }

        public Appeal(Resident resident, string content)
        {
            Resident = resident;
        }
        
        public Appeal(string name, Resident resident, string content)
        {
            Resident = resident;
            Content = content;
        }
        
        public IReadOnlyCollection<PropertyInfo> GetFields() => Properties;
    }
}