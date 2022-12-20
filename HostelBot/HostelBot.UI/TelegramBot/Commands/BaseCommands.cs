using HostelBot.App;

namespace HostelBot.Ui.TelegramBot.Commands;

public class BaseCommands : Commands
{
    public static Command StartCommand { get; private set; }

    public static void SetStartCommand(Command command)
    {
        StartCommand = command;
    }
}