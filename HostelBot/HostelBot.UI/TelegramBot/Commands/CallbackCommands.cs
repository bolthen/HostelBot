using HostelBot.App;

namespace HostelBot.Ui.TelegramBot.Commands;

public static class CallbackCommands
{
    private static Dictionary<string, Command> Name2Command { get; } = new();

    public static void AddCommands(IEnumerable<Command> commands)
    {
        foreach (var command in commands)
            AddCommand(command);
    }

    public static void AddCommand(Command command)
    {
        Name2Command[command.Name] = command;
    }

    public static bool Contains(string commandName)
    {
        return Name2Command.ContainsKey(commandName);
    }
    
    public static Command Get(string commandName)
    {
        return Name2Command[commandName];
    }
}