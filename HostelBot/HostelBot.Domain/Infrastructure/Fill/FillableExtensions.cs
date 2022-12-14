using System.Reflection;

namespace HostelBot.Domain.Infrastructure;

public static class FillableExtensions
{
    public static void FillClass(this IFillable fillableClass, IReadOnlyDictionary<string, string> data)
    {
        var propertiesToFill = fillableClass
            .GetFields()
            .Where(propertyInfo => propertyInfo.GetCustomAttribute<QuestionAttribute>() != null);
        foreach (var property in propertiesToFill)
        {
            if (!data.ContainsKey(property.Name))
                throw new ArgumentException($"Data do not contains {property.Name} key");
            property.SetValue(fillableClass, data[property.Name]);
        }
        
        fillableClass.OnFilled();
        //fillableClass.Filled = true; старая версия
    }
}