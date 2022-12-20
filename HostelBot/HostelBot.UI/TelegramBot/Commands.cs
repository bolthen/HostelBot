using HostelBot.App;

namespace HostelBot.Ui.TelegramBot;

public class BaseCommands : Commands
{
    public static Command StartCommand { get; private set; }

    public static void SetStartCommand(Command command)
    {
        StartCommand = command;
    }
}

public class CallbackCommands : Commands
{
}

public abstract class Commands
{
    private Dictionary<string, Command> Name2Command { get; } = new();

    public void AddCommands(IEnumerable<Command> commands)
    {
        foreach (var command in commands)
            Name2Command[command.Name] = command;
    }

    public IEnumerable<Command> GetCommands()
    {
        return Name2Command.Values;
    }
    
    public bool Contains(string commandName)
    {
        return Name2Command.ContainsKey(commandName);
    }
    
    public Command Get(string commandName)
    {
        return Name2Command[commandName];
    }
}