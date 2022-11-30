using System.Reflection;
using System.Text.Json;

namespace HostelBot.Domain.Infrastructure
{
    public interface ICanFill
    {
        IReadOnlyCollection<PropertyInfo> GetFields();
    }
}