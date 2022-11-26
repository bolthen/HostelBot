using System.Reflection;
using HostelBot.Domain.Infrastructure;

namespace HostelBot.Domain.Domain
{
    public class Resident : ValueType<Resident>, ICanFill
    {
        public IReadOnlyCollection<PropertyInfo> GetFields()
            => PropertiesToFill;
    }
}