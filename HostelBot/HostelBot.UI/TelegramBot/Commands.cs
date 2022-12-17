using HostelBot.App;

namespace HostelBot.Ui.TelegramBot;

public static class Commands
{
    private static Dictionary<long, UserCommands> ChatIdToUserCommands { get; } = new();

    public static Command StartCommand { get; private set; }

    public static void SetStartCommand(Command command)
    {
        StartCommand = command;
    }

    public static void AddUser(long chatId)
    {
        if (!ContainsUser(chatId))
            ChatIdToUserCommands[chatId] = new UserCommands();
    }

    public static bool ContainsUser(long chatId)
    {
        return ChatIdToUserCommands.ContainsKey(chatId);
    }

    public static void AddCommands(IEnumerable<Command> commands, long chatId)
    {
        ChatIdToUserCommands[chatId].AddCommands(commands);
    }

    public static bool Contains(string commandName, long chatId)
    {
        return ChatIdToUserCommands[chatId].Contains(commandName);
    }

    public static Command Get(string commandName, long chatId)
    {
        return ChatIdToUserCommands[chatId].Get(commandName);
    }

    public static void RegisterUser(long chatId)
    {
        ChatIdToUserCommands[chatId].RegisterUser();
    }

    public static bool IsUserRegistered(long chatId)
    {
        return ChatIdToUserCommands[chatId].UserRegistered;
    }
}

public class UserCommands
{
    private Dictionary<string, Command> NameToCommand { get; } = new();
    public bool UserRegistered { get; private set; } = false;
    
    public void AddCommands(IEnumerable<Command> commands)
    {
        foreach (var command in commands)
            NameToCommand[command.Name] = command;
    }

    public bool Contains(string commandName)
    {
        return NameToCommand.ContainsKey(commandName);
    }

    public Command Get(string commandName)
    {
        return NameToCommand[commandName];
    }
    
    public void RegisterUser()
    {
        UserRegistered = true;
    }
}