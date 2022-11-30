namespace HostelBot.Domain.Infrastructure;

public static class DictionaryExtensions
{
    public static string ToJsonFormat(this IReadOnlyDictionary<string, string> data)
    {
        var values = data
            .Select(item => $"\"{item.Key}\":\"{item.Value}\"");
        return $"{{{string.Join(',',values)}}}";
    }
}