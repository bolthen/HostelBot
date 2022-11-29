using System.Reflection;

namespace HostelBot.Domain.Infrastructure;

public class DddObject<TType>
    where TType : DddObject<TType>
{
    protected static readonly PropertyInfo[] Properties;
    
    static DddObject()
    {
        var type = typeof(TType);
        Properties = type.GetProperties(BindingFlags.Public |
                                        BindingFlags.Instance | 
                                        BindingFlags.DeclaredOnly)
            .Where(property => property.CanRead)
            .ToArray();
    }
}