﻿using HostelBot.App;

namespace HostelBot.Ui.TelegramBot.Commands;

public abstract class Commands
{
    private Dictionary<string, Command> Name2Command { get; } = new();

    public void AddCommands(IEnumerable<Command> commands)
    {
        foreach (var command in commands)
            AddCommand(command);
    }

    public void AddCommand(Command command)
    {
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