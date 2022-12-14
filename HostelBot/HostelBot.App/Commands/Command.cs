using HostelBot.Domain.Infrastructure;

namespace HostelBot.App
{
    public abstract class Command
    {
        public string Name { get; }

        public Command(string name)
        {
            Name = name;
        }

        public virtual List<Command> GetSubcommands()
        {
            return new List<Command>();
        }

        public virtual object[] GetStaticInfo()
        {
            return Array.Empty<object>();
        }

        public virtual IFillable? GetFillable()
        {
            return null;
        }
    }

    public class UtilityRegistration
    {
        public void AddUtility(string name)
        {
            
        }
    }
}