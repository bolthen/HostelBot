using System.Reflection;
using System.Text.Json;

namespace HostelBot.Domain.Infrastructure
{
    public interface IFillable
    {
        void OnFilled();
        IReadOnlyCollection<PropertyInfo> GetFields();
        long ResidentId { get; set; }
    }
}