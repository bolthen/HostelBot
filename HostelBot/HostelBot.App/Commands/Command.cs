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

        public virtual List<Command> GetSubcommands(int residentId)
        {
            return new List<Command>();
        }

        public virtual object[] GetStaticInfo(int residentId)
        {
            return Array.Empty<object>();
        }

        public virtual IFillable? GetFillable(int residentId)
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