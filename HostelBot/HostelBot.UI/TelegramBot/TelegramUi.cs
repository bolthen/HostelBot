using System;
using HostelBot.App;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace HostelBot.Ui.TelegramBot;

public class TelegramUi : IUi
{
    public TelegramUi(IApplication application)
    {
        commandsHelper = new CommandsHelper(application.GetBaseCommands());
    }

    public void Run()
    {
        const string token = "5675497155:AAHJO952wpubgQiIf3WdOJ6eCAi2cgnIKIs";
        var client = new TelegramBotClient(token);
        client.StartReceiving(Update, Error);
        
        Console.ReadLine();
    }

    private readonly CommandsHelper commandsHelper;

    private async Task Update(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        if (update.Type == UpdateType.CallbackQuery)
        {
            await Processor.HandleCallbackQuery(botClient, update.CallbackQuery!);
            return;
        }

        if (update.Type == UpdateType.Message && update.Message?.Text != null)
        {
            var text = update.Message.Text!;
            
            if (text == "/start")
            {
                await Processor.Start(botClient, update, cancellationToken, commandsHelper);
                return;
            }

            if (commandsHelper.NameToCommand.ContainsKey(text))
            {
                await Processor.HandleICommand(botClient, update, cancellationToken, commandsHelper.NameToCommand[text]);
                return;
            }
        }
        
        await botClient.SendTextMessageAsync(update.Message.Chat.Id, $"UpdateType: {update.Type}", 
            cancellationToken: cancellationToken);
    }

    private Task Error(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}