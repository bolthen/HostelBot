using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Text.Json.Serialization;
using HostelBot.Domain.Infrastructure;

namespace HostelBot.Domain.Domain
{
    public class Appeal : Entity<Appeal>, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        
        public Appeal() { }

        public Appeal( Resident resident, string content, RepositoryChangesParser repositoryChangesParser)
        {
            Resident = resident;
            Content = content;
            PropertyChanged += repositoryChangesParser.ParseRepositoryChanges;
        }
        
        public Resident Resident { get; set; }
        
        public string Content { get; set; }

        private string? answer;

        public string? Answer
        {
            get => answer;
            set
            {
                answer = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Answer)));
            }
        }

        public IReadOnlyCollection<PropertyInfo> GetFields() => Properties;
    }
}