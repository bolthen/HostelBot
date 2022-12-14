using System.Reflection;
using System.Text.Json;

namespace HostelBot.Domain.Infrastructure
{
    public interface IFillable
    {
        bool Filled { get; set; }
        void OnFilled();
        IReadOnlyCollection<PropertyInfo> GetFields();
    }
}