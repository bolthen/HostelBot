using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Text.Json.Serialization;
using HostelBot.Domain.Infrastructure;

namespace HostelBot.Domain.Domain
{
    public class Appeal : Entity<Appeal>
    {
        public Resident Resident { get; set;}
        
        public IReadOnlyCollection<PropertyInfo> GetFields() => Properties;

        public Appeal() { }
    
        public Appeal(string name, Resident resident)
        {
            Resident = resident;
            Name = name;
        }
        
        public Appeal(string name, Resident resident, string content)
        {
            Resident = resident;
            Name = name;
            Content = content;
        }
        
        public string Name { get; set; }

        public string Content { get; set; }
        
        public string Answer { get; set; }
    }
}