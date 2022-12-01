using System.Reflection;
using System.Text.Json;

namespace HostelBot.Domain.Infrastructure
{
    public interface IFillable
    {
        IReadOnlyCollection<PropertyInfo> GetFields();
    }
}