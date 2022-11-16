using System;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace HostelBot.Ui.TelegramBot;

public class TelegramUi : IUi
{
    public void Run()
    {
        const string token = "5675497155:AAHJO952wpubgQiIf3WdOJ6eCAi2cgnIKIs";
        var client = new TelegramBotClient(token);
        client.StartReceiving(Update, Error);
        
        Console.ReadLine();
    }

    private static async Task Update(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        if (update.Type == UpdateType.CallbackQuery)
        {
            await Processor.HandleCallbackQuery(botClient, update.CallbackQuery);
            return;
        }
        
        var message = update.Message;
        
        switch (message.Text)
        {
            case "/start":
                await Processor.Start(botClient, update, cancellationToken);
                return;
            case KeyboardButtons.Start.Info:
                await Processor.Info(botClient, update, cancellationToken);
                return;
            case KeyboardButtons.Start.Service:
                await Processor.Service(botClient, update, cancellationToken);
                return;
            // case KeyboardButtons.Start.Question:
            //     await Processor.Question(botClient, update, cancellationToken);
            //     return;
            // case KeyboardButtons.Start.Report:
            //     await Processor.Report(botClient, update, cancellationToken);
            //     return;
        }

        await botClient.SendTextMessageAsync(update.Message.Chat.Id, $"You said: {update.Message.Text}", 
            cancellationToken: cancellationToken);
    }

    private static Task Error(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

