using System.ComponentModel;
using System.Reflection;

namespace HostelBot.Domain.Infrastructure;

public static class FillableExtensions
{
    public static async void FillClass(this IFillable fillableClass, IReadOnlyDictionary<string, string> data)
    {
        var propertiesToFill = fillableClass
            .GetFields()
            .Where(propertyInfo => propertyInfo.GetCustomAttribute<QuestionAttribute>() != null);
        foreach (var property in propertiesToFill)
        {
            if (!data.ContainsKey(property.Name))
                throw new ArgumentException($"Data do not contains {property.Name} key");
            // здесь может быть ошибка, если заполняемый тип особенный
            var converter = TypeDescriptor.GetConverter(property.PropertyType);
            var result = converter.ConvertFrom(data[property.Name]);
            property.SetValue(fillableClass, result);
        }
        
        await fillableClass.OnFilled();
    }
}