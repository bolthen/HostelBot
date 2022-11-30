using HostelBot.App;

namespace HostelBot.Ui.TelegramBot;

public class CommandsHelper
{
    public Dictionary<string, ICommand> NameToCommand { get; } = new();

    public string[] Names { get; }

    public CommandsHelper(IReadOnlyCollection<ICommand> commands)
    {
        Names = commands.Select(x => x.Name).ToArray();

        foreach (var command in commands)
            NameToCommand[command.Name] = command;
    }

    public readonly Dictionary<long, FillingProgress> ChatIdToFillingProgress = new();
}