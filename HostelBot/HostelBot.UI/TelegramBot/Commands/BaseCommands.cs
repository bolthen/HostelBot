using HostelBot.App;

namespace HostelBot.Ui.TelegramBot.Commands;

internal static class BaseCommands
{
    public static Command StartCommand { get; private set; }

    public static void SetStartCommand(Command command)
    {
        StartCommand = command;
    }
    

    public static IEnumerable<Command> GetBaseCommands(long chatId)
    {
        return StartCommand.GetSubcommands(chatId);
    }

    public static IEnumerable<string> GetBaseCommandsNames(long chatId)
    {
        return GetBaseCommands(chatId).Select(x => x.Name);
    }
    
    public static bool Contains(string commandName, long chatId)
    {
        return GetBaseCommandsNames(chatId).Contains(commandName);
    }
    
    public static Command Get(string commandName, long chatId)
    {
        return GetBaseCommands(chatId).First(x => x.Name == commandName);
    }
}