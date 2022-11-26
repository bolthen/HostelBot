using System.Reflection;

namespace HostelBot.Domain.Domain
{
    public interface ICanFill
    {
        IReadOnlyCollection<PropertyInfo> GetFields();
    }
}