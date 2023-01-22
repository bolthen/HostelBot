namespace HostelBot.Domain.Infrastructure.Misc;

public static class DirectorySearch
{
    public static string? DeepFindSearch(string fileName, string lastDirectoryName="")
    {
        var currentPath = Environment.CurrentDirectory;

        while (currentPath is not null && Path.GetFileName(currentPath) != lastDirectoryName)
        {
            var file = Path.Combine(currentPath, fileName);
            if (Directory.Exists(file))
                return file;
            currentPath = Directory.GetParent(currentPath)?.FullName;
        }
        
        if (currentPath != null && lastDirectoryName.Length != 0)
            return Path.Combine(currentPath, fileName);
        return null;
    }
}