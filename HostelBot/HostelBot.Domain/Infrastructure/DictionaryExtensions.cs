using System.Reflection;

namespace HostelBot.Domain.Infrastructure;

public static class DictionaryExtensions
{
    public static string ToJsonFormat(this IReadOnlyDictionary<string, string> data)
    {
        var values = data
            .Select(item => $"\"{item.Key}\":\"{item.Value}\"");
        return $"{{{string.Join(',',values)}}}";
    }
    
    public static void FillClass(this IReadOnlyDictionary<string, string> data, IFillable fillableClass)
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
    }
}