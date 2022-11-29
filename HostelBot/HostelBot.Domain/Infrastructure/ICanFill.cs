using System.Reflection;

namespace HostelBot.Domain.Infrastructure
{
    public interface ICanFill
    {
        IReadOnlyCollection<PropertyInfo> GetFields();
    }
}