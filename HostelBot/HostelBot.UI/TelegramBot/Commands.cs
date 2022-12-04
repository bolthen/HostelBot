using HostelBot.App;

namespace HostelBot.Ui.TelegramBot;

public static class Commands
{
    private static Dictionary<string, Command> NameToCommand { get; } = new();

    public static IEnumerable<string> Names => NameToCommand.Keys;

    public static void AddCommands(IEnumerable<Command> commands)
    {
        foreach (var command in commands)
            NameToCommand[command.Name] = command;
    }

    public static bool Contains(string commandName)
    {
        return NameToCommand.ContainsKey(commandName);
    }

    public static Command Get(string commandName)
    {
        return NameToCommand[commandName];
    }
}